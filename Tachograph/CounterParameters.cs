using System;

namespace Tachograph
{
    public class CounterParameters
    {
        public int TotalKilometersDriven { get; private set; }
        public int Counter1 { get; private set; }
        public int Counter2 { get; private set; }
        public int Counter3 { get; private set; }
        public int Counter4 { get; private set; }
        public int Counter5 { get; private set; }

        public CounterParameters(int totalKilometersDriven, int counter1, int counter2, int counter3, int counter4, int counter5) 
        {
            TotalKilometersDriven = totalKilometersDriven;
            Counter1 = counter1;
            Counter2 = counter2;
            Counter3 = counter3;
            Counter4 = counter4;
            Counter5 = counter5;
        }
    }
}
