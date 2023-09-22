using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
        /// Metoda na kontrolu validity získaných parametrů ze SettingsPage a následného založení záznamu
        /// </summary>
        /// <param name="numericalTachoParameters"> číselné parametry tachografu </param>
        /// <param name="textTachoParameters"> textové parametry tachografu </param>
        /// <param name="signalParameters"> signály tachografu </param>
        public void AddRecord(int[] numericalTachoParameters, string[] textTachoParameters, bool[] signalParameters)
        {
            foreach (int p in numericalTachoParameters)
                if (p < 0)
                    throw new ArgumentException($"Záporná hodnota není povolena pro parametr {p}.");
            foreach (string p in textTachoParameters)
                if (string.IsNullOrEmpty(p)) // nebyl zadán
                    throw new ArgumentException("Některý z řetězcových parametrů nebyl zadán.");

            TachographRecord record = new (numericalTachoParameters, textTachoParameters, signalParameters);
            WriteData(record);

            MessageBox.Show("Parametry tachografu byly úspěšně zapsány.");
        }

        /// <summary>
        /// Metoda navazuje spojení s tafografem a řídí veškerou zapisovací komunikaci (ZATÍM ABSTRAKTNĚ)
        /// </summary>
        /// <param name="tachographRecord"> Vyplněné data tafografu na poslání </param>
        public async Task WriteData(TachographRecord tachographRecord)
        {
            try
            {
                using (UdpClient client = new UdpClient(sourcePort))
                {
                    IPAddress tachographAddress = IPAddress.Parse(destinationIP);
                    IPEndPoint tachographEndPoint = new IPEndPoint(tachographAddress, destinationPort);

                    // Převedení vyplněných dat tachografu na bajty
                    byte[] recordData = tachographRecord.ToBytes();
                    // Výpočet CRC-32 pro data
                    uint crc32 = CalculateCRC32(recordData);

                    // Spojení prefixu, CRC-32 a dat do jednoho pole bajtů
                    byte[] writeData = BitConverter.GetBytes(writingPrefix);
                    writeData = writeData.Concat(BitConverter.GetBytes(crc32)).ToArray();
                    writeData = writeData.Concat(recordData).ToArray();

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(writeData);

                    await client.SendAsync(writeData, writeData.Length, tachographEndPoint);

                    // Přijmutí odpovědi na čtení dat (simulace)
                    var receiveResult = await client.ReceiveAsync();
                    byte[] receivedBytes = receiveResult.Buffer;

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(receivedBytes);

                    int responseData = BitConverter.ToInt32(receivedBytes, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Výpočet kontrolního součtu CRC32
        /// </summary>
        /// <param name="data"> data na zahashování </param>
        /// <returns> Vrací výsledek výpočtu kontrolního součtu CRC32 (Cyclic Redundancy Check) pro vstupní pole bajtů </returns>
        uint CalculateCRC32(byte[] data)
        {
            // Nejprve je inicializována proměnná crc na hodnotu 0xFFFFFFFF.Toto je počáteční hodnota pro výpočet CRC-32.CRC - 32 je kontrolní součet, který se postupně upravuje během výpočtu.
            uint crc = 0xFFFFFFFF; // Počáteční hodnota CRC

            foreach (byte b in data)
            {
                // V každém kroku cyklu se provede operace XOR mezi aktuální hodnotou crc a bytem b z pole data. Toto zajišťuje kombinaci aktuálního stavu CRC s novými daty.
                crc ^= b; // XOR operace s bytem 
                // Po provedení XOR operace s bytem b následuje vnitřní cyklus, který provádí operace na každém jednom bitu v osmi bitech byteu. V tomto cyklu se používá bitová maska a logické operace.
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 1) == 1) // Pro každý bit v byteu b se kontroluje, zda je nejnižší bit (LSB) nastaven (roven 1).
                        // Pokud je nejnižší bit 1, pak se provádí posun hodnoty crc o jedno místo doprava (bitový posun vpravo).
                        // Poté se provede XOR této hodnoty s konstantou 0xEDB88320.
                        // Tato konstanta je standardní hodnota pro CRC-32 a slouží k tomu, aby byl výpočet efektivní a robustní.
                        crc = (crc >> 1) ^ 0xEDB88320; // XOR a posun vpravo s konstantou
                    else
                        //Pokud je nejnižší bit 0, pouze se provádí bitový posun hodnoty crc o jedno místo doprava.
                        crc >>= 1; // Pouze posun vpravo
                }
            }
            // Po zpracování všech bitů v byteu b se pokračuje s dalším bytem v poli data. Po dokončení všech bytů se výsledná hodnota crc invertuje pomocí operace ~crc.
            return ~crc; // Invertování výsledného CRC
        }
    }
}
