using System;

namespace Tachograph
{
    public class TachographParameters
    {
        public int WheelDiameter { get; private set; }
        public int CarNumber { get; private set; }
        
        public TachographParameters(int wheelDiameter, int carNumber)
        {
            WheelDiameter = wheelDiameter;
            CarNumber = carNumber;
        }
    }
}
