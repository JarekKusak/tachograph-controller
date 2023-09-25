using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachograph
{
    public class SignalParameters
    {
        public bool[] ActiveSignals { get; private set; }
        public bool[] BreakSignals { get; private set; }
        public bool[] InverseSignals { get; private set; }
        private bool[] allSignals;

        public SignalParameters(bool[] allSignals) 
        {
            this.allSignals = allSignals;
            ActiveSignals = new bool[allSignals.Length / 3];
            BreakSignals = new bool[allSignals.Length / 3];
            InverseSignals = new bool[allSignals.Length / 3];
        }

        /// <summary>
        /// Nastaví do proměnných signálů daného typu označené signály
        /// </summary>
        public void SetSignalParameters()
        {
            int signalIndex = 0;  // Počáteční index v poli allSignals

            // Nastavíme hodnoty v poli ActiveSignals z první třetiny allSignals
            for (int i = 0; i < ActiveSignals.Length; i++)
            {
                ActiveSignals[i] = allSignals[signalIndex];
                signalIndex++;  // Zvýšíme index v allSignals
            }
            // Nastavíme hodnoty v poli BreakSignals ze druhé třetiny allSignals
            for (int i = 0; i < BreakSignals.Length; i++)
            {
                BreakSignals[i] = allSignals[signalIndex];
                signalIndex++;  // Zvýšíme index v allSignals
            }
            // Nastavíme hodnoty v poli InverseSignals z poslední třetiny allSignals
            for (int i = 0; i < InverseSignals.Length; i++)
            {
                InverseSignals[i] = allSignals[signalIndex];
                signalIndex++;  // Zvýšíme index v allSignals
            }
        }
    }
}
