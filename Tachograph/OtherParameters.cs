using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachograph
{
    public class OtherParameters
    {
        public int Mode { get; private set; }
        public float RecordStep { get; private set; }
        public string SpeedRecordType { get; private set; }
        public string TachographType { get; private set; }

        public OtherParameters(int mode, float recordStep, string speedRecordType, string tachographType) 
        {
            Mode = mode;
            RecordStep = recordStep;   
            SpeedRecordType = speedRecordType;
            TachographType = tachographType;
        }    
    }
}