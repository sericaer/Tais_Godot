using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using static Tais.API.Method;
namespace Native.Adjust
{
    public class TAX_LEVEL : AdjustTaxDef
    {
        public override decimal[] level_rates => ARRAY(
            0M,
            0.0001M,
            0.0002M,
            0.0003M,
            0.0004M,
            0.0005M,
            0.0006M,
            0.0007M,
            0.0008M,
            0.0009M,
            0.001M
            );

        public override int init_level => 1;
    }
}
