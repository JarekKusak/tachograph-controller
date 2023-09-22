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
        /// Metoda na kontrolu validity získaných parametrů ze SettingsPage a následného založení záznamu
        /// </summary>
        /// <param name="numericalTachoParameters"> číselné parametry tachografu </param>
        /// <param name="textTachoParameters"> textové parametry tachografu </param>
        /// <param name="signalParameters"> signály tachografu </param>
        public void AddRecord(int?[] numericalTachoParameters, string[] textTachoParameters, bool[] signalParameters)
        {
            foreach (int p in numericalTachoParameters)
                if (p < 0)
                    throw new ArgumentException($"Záporná hodnota není povolena pro parametr {p}.");
            foreach (string p in textTachoParameters)
                if (string.IsNullOrEmpty(p)) // nebyl zadán
                    throw new ArgumentException("Některý z řetězcových parametrů nebyl zadán.");
            MessageBox.Show("vše OK ;-)");
            //TaphographRecord record = new TaphographRecord();
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

                    Console.WriteLine($"Odesílání zápisu dat: {BitConverter.ToString(writeData)}");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        uint CalculateCRC32(byte[] data)
        {
            uint crc = 0xFFFFFFFF; // Počáteční hodnota CRC

            foreach (byte b in data)
            {
                crc ^= b; // XOR operace s bytem
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 1) == 1)
                        crc = (crc >> 1) ^ 0xEDB88320; // XOR a posun vpravo s konstantou
                    else
                        crc >>= 1; // Pouze posun vpravo
                }
            }

            return ~crc; // Invertování výsledného CRC
        }
    }
}
