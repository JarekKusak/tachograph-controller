using System;

namespace Tachograph
{
    public class CarParameters
    {
        public string CarType { get; private set; }
        public int GearRatio { get; private set; }
        public int MaxWheelDiameter { get; private set; }
        public int MaxSpeed { get; private set; }
        public int KFactor { get; private set; }

        public CarParameters(string carType, int gearRatio, int maxWheelDiameter, int maxSpeed, int kFactor) 
        { 
            CarType = carType;
            GearRatio = gearRatio;
            MaxWheelDiameter = maxWheelDiameter;
            MaxSpeed = maxSpeed;
            KFactor = kFactor;
        }
    }
}
