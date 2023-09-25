using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachograph
{
    internal class TachographParameters
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
