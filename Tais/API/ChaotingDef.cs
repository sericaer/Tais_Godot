using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public abstract class ChaotingDef
    {
        public abstract decimal[] taxRates { get; }

        public abstract PowerStatus[] powerStatusArray { get; }
    }

    public class PowerStatus
    {
        public string desc;
        public (decimal min, decimal max) powerRange;

        public Effect effect;

        public class Effect
        {
            public int min_chaoting_report_tax_level;
        }

        public bool isValid(decimal powerValue)
        {
            return powerValue <= powerRange.max && powerValue >= powerRange.min;
        }
        
    }

    public interface EFFECT_MIN_LEVEL_CHAOTING_REPORT_TAX
    {
        int value { get; }
    }
}
