using System;
using System.IO;
using System.Text;

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

            Můžeme reprezentovat signály jako pole boolů, kde každý bool je jeden byte. Pokud máme 3x bool[48], pak potřebujeme 144 bytů.
            Další byty:

            (Zde můžete zahrnout další byty, které potřebujete pro vaši aplikaci)
         */

        // Datum a čas
        string dateAndTime;

        // Prefix (4 byty)
        // Blok parametry tacho:
        int wheelDiameter; // 4 byty
        int carNumber; // 1 byte
        // Blok parametry vozu:
        string carType; // // dvě proměnné: délka stringu (4 byty) + samotný string (? bytů)
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

        // Nezapisuje se
        // int mode; // 1 byte

        string speedRecordType; // dvě proměnné: délka stringu (4 byty) + samotný string (? bytů)

        // Nezapisují se
        // float recordStep; // 4 byty

        // string taphographType; // dvě proměnné: délka stringu (4 byty) + samotný string (? bytů)

        bool[] activeSignals;
        bool[] breakSignals;
        bool[] inverseSignals;

        // + byty navíc..

        bool writeDownTachoParameters = false;
        bool writeDownCarParameters = false;
        bool writeDownCounterParameters = false;
        bool writeDownSignalParameters = false;
        bool writeDownDateAndTime = false;

        // Každý konstruktor představuje různý typ záznamu na zápis

        public TachographRecord(string dateAndTime)
        {
            this.dateAndTime = dateAndTime;
            writeDownDateAndTime = true;
        }

        public TachographRecord(TachographParameters tachographParameters)
        {
            wheelDiameter = tachographParameters.WheelDiameter;
            carNumber = tachographParameters.CarNumber;

            writeDownTachoParameters = true;
        }

        public TachographRecord(CarParameters carParameters, SignalParameters signalParameters, OtherParameters otherParameters)
        {
            carType = carParameters.CarType;
            gearRatio = carParameters.GearRatio;
            maxWheelDiameter = carParameters.MaxSpeed;
            maxSpeed = carParameters.MaxSpeed;
            kFactor = carParameters.KFactor;

            activeSignals = signalParameters.ActiveSignals;
            breakSignals = signalParameters.BreakSignals;
            inverseSignals = signalParameters.InverseSignals;

            speedRecordType = otherParameters.SpeedRecordType;

            writeDownCarParameters = true;
            writeDownSignalParameters = true; // signály se připojují k parametrům vozu
        }

        public TachographRecord(CounterParameters counterParameters)
        {
            totalKilometersDriven = counterParameters.TotalKilometersDriven;
            counter1 = counterParameters.Counter1;
            counter2 = counterParameters.Counter2;
            counter3 = counterParameters.Counter3;
            counter4 = counterParameters.Counter4;
            counter5 = counterParameters.Counter5;

            writeDownCounterParameters = true;
        }

        // Univerzální kontruktor na zápis všeho (nepoužívá se)
        /*
        public TachographRecord(TachographParameters tachographParameters, CarParameters carParameters, CounterParameters counterParameters, OtherParameters otherParameters, 
            SignalParameters signalParameters, string dateAndTime) 
        {
            this.dateAndTime = dateAndTime;
            wheelDiameter = tachographParameters.WheelDiameter;
            carNumber = tachographParameters.CarNumber;

            carType = carParameters.CarType;
            gearRatio = carParameters.GearRatio;
            maxWheelDiameter = carParameters.MaxSpeed;
            maxSpeed = carParameters.MaxSpeed;
            kFactor = carParameters.KFactor;
            
            totalKilometersDriven = counterParameters.TotalKilometersDriven;
            counter1 = counterParameters.Counter1;
            counter2 = counterParameters.Counter2;
            counter3 = counterParameters.Counter3;
            counter4 = counterParameters.Counter4;
            counter5 = counterParameters.Counter5;
            
            // mode = otherParameters.Mode;
            // recordStep = otherParameters.RecordStep;

            speedRecordType = otherParameters.SpeedRecordType;
            // taphographType = otherParameters.TachographType;

            activeSignals = signalParameters.ActiveSignals;
            breakSignals = signalParameters.BreakSignals;
            inverseSignals = signalParameters.InverseSignals;

            writeDownDateAndTime = writeDownTachoParameters = writeDownSignalParameters = writeDownCarParameters = writeDownCounterParameters = true;
        }
        */

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
                // Parametry typ tachografu a mód se nezapisují
                /*
                byte[] taphographTypeBytes = Encoding.UTF8.GetBytes(taphographType);

                writer.Write(taphographTypeBytes);
                writer.Write(taphographTypeBytes.Length);
                writer.Write(recordStep);

                // Další parametry:
                
                writer.Write((byte)mode);
                */
                if (writeDownDateAndTime)
                {
                    byte[] dateAndTimeBytes = Encoding.UTF8.GetBytes(dateAndTime);
                    /*
                    // Převedení bytů z malého endianu na velký endian
                    byte[] reversedDateAndTimeBytes = new byte[dateAndTimeBytes.Length];
                    for (int i = 0; i < dateAndTimeBytes.Length; i++)
                    {
                        reversedDateAndTimeBytes[i] = dateAndTimeBytes[dateAndTimeBytes.Length - 1 - i];
                    }
                    writer.Write(reversedDateAndTimeBytes);
                    */
                    writer.Write(dateAndTimeBytes);
                    writer.Write(dateAndTimeBytes.Length);
                }  
                if (writeDownCounterParameters)
                {
                    // Blok počítadla:
                    writer.Write((byte)counter5);
                    writer.Write((byte)counter4);
                    writer.Write((byte)counter3);
                    writer.Write((byte)counter2);
                    writer.Write((byte)counter1);
                    writer.Write(totalKilometersDriven);
                }
                if (writeDownSignalParameters)
                {
                    // Blok parametry vozu:
                    // Signály
                    foreach (bool b in inverseSignals)
                        writer.Write(b);
                    foreach (bool b in breakSignals)
                        writer.Write(b);
                    foreach (bool b in activeSignals)
                        writer.Write(b);
                }
                if (writeDownCarParameters)
                {
                    byte[] speedRecordTypeBytes = Encoding.UTF8.GetBytes(speedRecordType);
                    writer.Write(speedRecordTypeBytes);
                    writer.Write(speedRecordTypeBytes.Length);

                    writer.Write(kFactor);
                    writer.Write(maxSpeed);
                    writer.Write(maxWheelDiameter);
                    writer.Write(gearRatio);
                    byte[] carTypeBytes = Encoding.UTF8.GetBytes(carType);
                    writer.Write(carTypeBytes);
                    writer.Write(carTypeBytes.Length);
                }            
                if (writeDownTachoParameters)
                {
                    // Blok parametry tacho:
                    writer.Write((byte)carNumber);
                    writer.Write(wheelDiameter);
                }

                writer.Write(writingPrefix);

                // Další byty (zde můžete implementovat logiku pro zápis dalších bytů)
                
                // Metoda ToArray() na instanci MemoryStream slouží k získání obsahu tohoto bufferu jako pole bajtů (byte[]).
                // To znamená, že pokud jste zapsali nějaká data do MemoryStream, můžete tato data získat pomocí metody ToArray() ve formě pole bajtů.
                return stream.ToArray();
            }
        }
    }
}
