using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tachograph
{
    class WritingInterface
    {
        string destinationIP;
        int sourcePort;
        int destinationPort;
        int writingPrefix;

        public WritingInterface(string destinationIP, int sourcePort, int destinationPort) 
        {
            this.destinationIP = destinationIP;
            this.sourcePort = sourcePort;
            this.destinationPort = destinationPort;

            writingPrefix = 0x15100000;
        }

        /// <summary>
        /// Metoda navazuje spojení s tafografem a řídí veškerou zapisovací komunikaci (ZATÍM ABSTRAKTNĚ)
        /// </summary>
        /// <param name="taphographRecord"> Vyplněné data tafografu na poslání </param>
        async Task WriteData(TaphographRecord taphographRecord)
        {
            try
            {
                using (UdpClient client = new UdpClient(sourcePort))
                {
                    IPAddress tachographAddress = IPAddress.Parse(destinationIP);
                    IPEndPoint tachographEndPoint = new IPEndPoint(tachographAddress,destinationPort);

                    // Simulace zápisu dat
                    int dataToWrite = 42; // Vaše data, která chcete zapsat
                    byte[] writeData = BitConverter.GetBytes(writingPrefix | dataToWrite);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(writeData);

                    Console.WriteLine($"Odesílání zápisu dat: {dataToWrite}");
                    await client.SendAsync(writeData, writeData.Length, tachographEndPoint);

                    // Přijmutí odpovědi na čtení dat (simulace)
                    var receiveResult = await client.ReceiveAsync();
                    byte[] receivedBytes = receiveResult.Buffer;

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(receivedBytes);

                    int responseData = BitConverter.ToInt32(receivedBytes, 0);

                    Console.WriteLine($"Přijata odpověď na čtení dat: {responseData}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
