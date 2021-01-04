using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using static Tais.API.Method;

namespace Native
{
    public class Chaoting : ChaotingDef
    {
        public override decimal[] taxRates => ARRAY(
            0.001M,
            0.002M,
            0.003M,
            0.004M,
            0.005M,
            0.006M,
            0.007M
            );
    }
}
