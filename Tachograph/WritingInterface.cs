using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        }   
        
        static async Task WriteData()
        {
            string tachographIp = "192.168.30.15";
            int tachographPort = 5049;

            try
            {
                using (UdpClient client = new UdpClient())
                {
                    IPAddress tachographAddress = IPAddress.Parse(tachographIp);
                    IPEndPoint tachographEndPoint = new IPEndPoint(tachographAddress, tachographPort);

                    // Abstraktní prefixy pro čtení a zápis
                    int readPrefix = 0x15000000;
                    int writePrefix = 0x15100000;

                    // Simulace zápisu dat
                    int dataToWrite = 42; // Vaše data, která chcete zapsat
                    byte[] writeData = BitConverter.GetBytes(writePrefix | dataToWrite);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(writeData);

                    Console.WriteLine($"Odesílání zápisu dat: {dataToWrite}");
                    await client.SendAsync(writeData, writeData.Length, tachographEndPoint);

                    // Simulace čtení dat
                    byte[] readData = BitConverter.GetBytes(readPrefix);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(readData);

                    Console.WriteLine("Odesílání požadavku na čtení dat.");
                    await client.SendAsync(readData, readData.Length, tachographEndPoint);

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
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
