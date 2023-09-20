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

        int mode;

        string speedRecordType;

        int recordStep;

        string taphographType;

        // + signály (3x bool[48])
        // + byty navíc..

        public TaphographRecord(int wheelDiameter, int carNumber, int gearRatio, int maxWheelDiameter, int maxSpeed, int kFactor, int totalKilometersDriven, 
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
