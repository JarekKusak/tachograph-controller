﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tachograph
{
    class TachographRecord
    {
        /*
            -------- Zápis dat do tachografu --------

            Začne se s prefixem 0x15100000 jako 4 bajty (32 bity).
            Blok parametry tacho:

            wheelDiameter jako 4 bajty (int)
            carNumber jako 1 byte (int)

            Blok parametry vozu:

            carType jako řetězec - nejdříve zapište délku řetězce jako 4 bajty (int) a pak samotný řetězec.
            gearRatio jako 4 bajty (int)
            maxWheelDiameter jako 4 bajty (int)
            maxSpeed jako 4 bajty (int)
            kFactor jako 4 bajty (int)

            Blok počítadla:

            totalKilometersDriven jako 4 bajty (int)
            counter1, counter2, counter3, counter4 a counter5 každý jako 1 byte (int)
            
            Další parametry:

            mode jako 1 byte (int)
            speedRecordType jako řetězec - nejdříve délka jako 4 bajty (int) a pak řetězec.
            recordStep jako 4 bajty (int)
            taphographType jako řetězec - nejdříve délka jako 4 bajty (int) a pak řetězec.
            
            Signály:

            Můžeme reprezentovat signály jako pole boolů, kde každý bool je jeden bit. Pokud máme 3x bool[48], pak potřebujeme 144 bitů.
            Další byty:

            Zde byste měli zahrnout další byty, které potřebujete pro vaši aplikaci.
         */

        // Prefix (4 byty)
        // Blok parametry tacho:
        int wheelDiameter; // 4 byty
        int carNumber; // 1 byte
        // Blok parametry vozu:
        string carType; // ? bytů
        int gearRatio; // 4 byty
        int maxWheelDiameter; // 4 byty
        int maxSpeed; // km/h // 4 byty
        int kFactor; // 4 byty
        // Blok počítadla:
        int totalKilometersDriven; // 4 byty
        int counter1; // 1 byte
        int counter2; // 1 byte
        int counter3; // 1 byte
        int counter4; // 1 byte
        int counter5; // 1 byte

        int mode; // 1 byte

        string speedRecordType; // ? bytů

        int recordStep; // ? bytů (asi by byl vhodnější float)

        string taphographType; // ? bytů

        bool[] signalParameters;
        // + byty navíc..

        public TachographRecord(int[] numericalTachoParameters, string[] textTachoParameters, bool[] signalParameters) 
        {
            wheelDiameter = numericalTachoParameters[0];
            carNumber = numericalTachoParameters[1];
            gearRatio = numericalTachoParameters[2];
            maxWheelDiameter = numericalTachoParameters[3];
            maxSpeed = numericalTachoParameters[4];
            kFactor = numericalTachoParameters[5];
            totalKilometersDriven = numericalTachoParameters[6];
            counter1 = numericalTachoParameters[7];
            counter2 = numericalTachoParameters[8];
            counter3 = numericalTachoParameters[9];
            counter4 = numericalTachoParameters[10];
            counter5 = numericalTachoParameters[11];
            mode = numericalTachoParameters[12];
            recordStep = numericalTachoParameters[13];

            carType = textTachoParameters[0];
            speedRecordType = textTachoParameters[1];
            taphographType = textTachoParameters[2];
            this.signalParameters = signalParameters;
        }

        /// <summary>
        /// Metoda zapisuje byty do streamu (v opačném pořadí) a následně vrací pole bytů na zápis
        /// </summary>
        /// <param name="writingPrefix"> Prefix pro žádost na zápis do tachografu </param>
        /// <returns> Pole bytů na zápis </returns>
        public byte[] ToBytes(int writingPrefix)
        {
            // třída, která vytváří stream dat v paměti RAM
            // umožňuje efektivně zapisovat a číst data, aniž by bylo nutné je ukládat do souboru nebo jiného trvalého úložiště.
            using (MemoryStream stream = new MemoryStream())
            // třída, která zjednodušuje zápis primitivních datových typů (jako int, byte, float, atd.) do streamu
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // na signalParameters je voláno Reverse, aby signály byly v pořadí Aktivní, Brzdné a Inverzní
                foreach (bool b in signalParameters.Reverse())
                    writer.Write(b);

                byte[] taphographTypeBytes = Encoding.UTF8.GetBytes(taphographType);

                writer.Write(taphographTypeBytes);
                writer.Write(taphographTypeBytes.Length);
                writer.Write(recordStep);

                // Další parametry:
                byte[] speedRecordTypeBytes = Encoding.UTF8.GetBytes(speedRecordType);
                writer.Write(speedRecordTypeBytes);
                writer.Write(speedRecordTypeBytes.Length);
                writer.Write((byte)mode);

                // Blok počítadla:
                writer.Write((byte)counter5);
                writer.Write((byte)counter4);
                writer.Write((byte)counter3);
                writer.Write((byte)counter2);
                writer.Write((byte)counter1);
                writer.Write(totalKilometersDriven);

                // Blok parametry vozu:
                writer.Write(kFactor);
                writer.Write(maxSpeed);
                writer.Write(maxWheelDiameter);
                writer.Write(gearRatio);
                byte[] carTypeBytes = Encoding.UTF8.GetBytes(carType);
                writer.Write(carTypeBytes);
                writer.Write(carTypeBytes.Length); 

                // Blok parametry tacho:
                writer.Write((byte)carNumber);
                writer.Write(wheelDiameter);

                writer.Write(writingPrefix);

                // Další byty (zde můžete implementovat logiku pro zápis dalších bytů)
                
                // Metoda ToArray() na instanci MemoryStream slouží k získání obsahu tohoto bufferu jako pole bajtů (byte[]).
                // To znamená, že pokud jste zapsali nějaká data do MemoryStream, můžete tato data získat pomocí metody ToArray() ve formě pole bajtů.
                return stream.ToArray();
            }
        }
    }
}
