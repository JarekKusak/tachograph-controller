using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            counter1, counter2, counter3, counter4, a counter5 každý jako 1 byte (int)
            
            Další parametry:

            mode jako 1 byte (int)
            speedRecordType jako řetězec - nejdříve délka jako 4 bajty (int) a pak řetězec.
            recordStep jako 4 bajty (int)
            taphographType jako řetězec - nejdříve délka jako 4 bajty (int) a pak řetězec.
            
            Signály:

            Můžeme reprezentovat signály jako pole boolů, kde každý bool je jeden bit. Pokud máte 3x bool[48], pak potřebujete 144 bitů.
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

        // + signály (3x bool[48])
        // + byty navíc..

        public TachographRecord(int wheelDiameter, int carNumber, int gearRatio, int maxWheelDiameter, int maxSpeed, int kFactor, int totalKilometersDriven, 
            int counter1, int counter2, int counter3, int counter4, int counter5, int mode, int recordStep, string carType, string speedRecordType, string taphographType) 
        {
            this.wheelDiameter = wheelDiameter;
            this.carNumber = carNumber;
            this.carType = carType;
            this.gearRatio = gearRatio;
            this.maxWheelDiameter = maxWheelDiameter;
            this.maxSpeed = maxSpeed;
            this.kFactor = kFactor;
            this.totalKilometersDriven = totalKilometersDriven;
            this.counter1 = counter1;
            this.counter2 = counter2;
            this.counter3 = counter3;
            this.counter4 = counter4;
            this.counter5 = counter5;
            this.mode = mode;
            this.speedRecordType = speedRecordType;
            this.recordStep = recordStep;
            this.taphographType = taphographType;
        }         
    }
}
