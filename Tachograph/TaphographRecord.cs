using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachograph
{
    class TaphographRecord
    {
        // Blok parametry tacho:
        int wheelDiameter;
        int carNumber;
        // Blok parametry vozu:
        string carType;
        int gearRatio;
        int maxWheelDiameter;
        int maxSpeed; // km/h
        int kFactor;
        // Blok počítadla:
        int totalKilometersDriven;
        int counter1;
        int counter2;
        int counter3;
        int counter4;
        int counter5;
        // Blok módy:
        int mode0;
        int mode1;
        int mode2;
        int mode3;

        int speedRecordType;

        int recordStep;

        string taphographType;
        public TaphographRecord(int wheelDiameter, int carNumber, int carType, int gearRatio, int maxWheelDiameter, int maxSpeed) 
        { 
            
        }         
    }
}
