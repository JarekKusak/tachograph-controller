using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Windows;

namespace Tachograph
{
    /// <summary>
    /// Třída, která čte data z tafografu za pomocí UDP protokolu a následně je zapisuje do souboru.
    /// </summary>
    internal class ReadingInterface
    {
        const int dataLength = 517;
        const int timeout = 200; // timeout na 200 milisekund
        const int maxPacketIndex = 16384;
        const int maxRetries = 3;
        int retries;
        int readingPrefix;
        int packetIndex;
        int sourcePort;
        int destinationPort;
        string destinationIP;
        string filePath;
        string projectDirectory;
        string outputFileName;
        StreamWriter writer;
        
        // Importování funkce SendARP z knihovny iphlpapi.dll
        // Toto je atribut označující externí funkci, který říká, že tuto funkci hledáme v knihovně iphlpapi.dll.
        // ExactSpelling = true říká, že by měla být použita přesně tato název funkce, bez ohledu na velikost písmen.
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIp, byte[] macAddress, ref int macAddressLength);

        public ReadingInterface(string destinationIP, int sourcePort, int destinationPort)
        {
            readingPrefix = 0x15000000;
            packetIndex = 1;
            retries = 0;

            outputFileName = "output.txt";

            this.destinationIP = destinationIP;
            this.sourcePort = sourcePort;
            this.destinationPort = destinationPort;

            // Získejte cestu k adresáři projektu, kde chcete vytvořit soubor "output.txt"
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Sestavte úplnou cestu k souboru "output.txt"
            filePath = Path.Combine(projectDirectory, outputFileName);
        }      

        /// <summary>
        /// Pokus o zaslání ARP packetu
        /// </summary>
        /// <returns> Při selhání vrací false </returns>
        bool TryToSendARP()
        {
            // Získání MAC adresy pro Tachograph pomocí ARP dotazu
            byte[] macAddress = GetMacAddress(destinationIP);
            if (macAddress != null)
            {
                Console.WriteLine("ARP dotaz byl úspěšný.");
                string macAddressStr = string.Join(":", macAddress.Select(b => b.ToString("X2")));
                Console.WriteLine($"MAC adresa pro {destinationIP}: {macAddressStr}");
            }
            else
            {
                MessageBox.Show("Chyba v komunikaci - ARP dotaz selhal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Pokud ARP selže, vrátíme chybu
            }
            return true;
        }    

        /// <summary>
        /// Metoda na asynchronní zasílání a přijímání packetů za pomocí UDP protokolu (s veškerou režií)
        /// </summary
        async public Task ReadData(ProgressBar progressBar)
        {
            writer = new StreamWriter(filePath);

            try
            {
                using (UdpClient client = new UdpClient(sourcePort)) // inicializace UDP klienta na řízení komunikace
                {
                    IPAddress tachographAddress = IPAddress.Parse(destinationIP);
                    IPEndPoint tachographEndPoint = new IPEndPoint(tachographAddress, destinationPort);

                    if (TryToSendARP()) // pokud zaslání ARP packetu proběhne úspěšně
                    {
                        while (packetIndex <= maxPacketIndex)
                        {
                            if (retries < maxRetries) // pokud se překročí počet opakovaných pokusů o zaslání packetu, vypadne výjimka (např. při výpadku sítě)
                            {
                                byte[] data = BitConverter.GetBytes(readingPrefix);

                                if (BitConverter.IsLittleEndian) // převedení little endian bytů na big endianitu (síťové protokoly standardně jsou big endian)
                                    Array.Reverse(data);

                                var sendTask = client.SendAsync(data, data.Length, tachographEndPoint); // zaslání požadavku na packet
                                Console.WriteLine($"Žádost na packet č.{packetIndex}");
                                // Počkejte na odeslání
                                await sendTask;

                                byte[] receiveData = null;

                                using (var cts = new CancellationTokenSource()) // cancellation token na správné ukončení asynchronních vláken
                                {
                                    var cancellationToken = cts.Token;
                                    var receiveTask = client.ReceiveAsync(cancellationToken).AsTask(); // Předáme CancellationToken do metody ReceiveAsync a převedeme na Task

                                    if (await Task.WhenAny(receiveTask, Task.Delay(timeout, cancellationToken)) == receiveTask) // pokud doběhne první receiveTask, data se obdržely, jinak timeout
                                    {
                                        // Odpověď byla úspěšně přijata
                                        receiveData = receiveTask.Result.Buffer;
                                        retries = 0;
                                        cts.Cancel(); // Zrušíme CancellationToken pro bezpečné ukončení timeoutTask
                                    }
                                    else // timeout
                                    {
                                        Console.WriteLine($"Timeout pro požadavek č.{packetIndex}");
                                        retries++;
                                        cts.Cancel(); // Zrušíme CancellationToken pro případné použití v dalším kódu
                                        continue;
                                    }
                                }

                                if (receiveData != null && receiveData.Length == dataLength) // při úspěšném obdržení packetu se data zapíšou do souboru
                                {
                                    Console.WriteLine($"Obdrženo č.{packetIndex}");
                                    PacketOutput(receiveData, packetIndex, writer);
                                    packetIndex++;
                                    readingPrefix++; // navýšení hodnoty pro další požadavek

                                    // Aktualizace ProgressBar na UI vlákně
                                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => // asynchronní řízení progress baru
                                    {
                                        progressBar.Value = (double)packetIndex / maxPacketIndex * 100;
                                    }));
                                }
                                else Console.WriteLine("UDP packet fail, zkusíme to znovu...");
                            }
                            else // překročení maximálního počtu opakovaných pokusů o žádost na packet
                            {
                                MessageBox.Show("Chyba v komunikaci - překročen počet pokusů o zaslání packetu.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        MessageBox.Show($"Packety byly přečteny a zapsány do {filePath}.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
                    } else return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Metoda na tisknutí bytů přijatých dat (podoba hexdumpu)
        /// </summary>
        /// <param name="data"> Obdržená data v packetu </param>
        void PacketOutput(byte[] data, int packetIndex, StreamWriter writer)
        {
            int i = 0;
            int rowWidth = 16;

            foreach (byte b in data)
            {
                if (i % rowWidth == 0)
                {
                    writer.Write("\n");
                    i = 0;
                }
                writer.Write(b.ToString("X2") + " "); // output je v šestnáctkové soustavě
                i++;
            }
        }

        /// <summary>
        /// Metoda pro provádění ARP dotazu a vrácení MAC adresy
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns> Mac adresa </returns>
        byte[] GetMacAddress(string ipAddress)
        {
            try
            {
                int targetIp = BitConverter.ToInt32(IPAddress.Parse(ipAddress).GetAddressBytes(), 0);
                byte[] macAddress = new byte[6];
                int macAddressLength = macAddress.Length;
                int result = SendARP(targetIp, 0, macAddress, ref macAddressLength);

                if (result == 0) return macAddress; 
                else return null;
            }
            catch
            {
                return null;
            }
        }    
    }
}
