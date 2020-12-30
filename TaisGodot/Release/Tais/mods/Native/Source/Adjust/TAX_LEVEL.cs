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
            0.001M,
            0.002M,
            0.003M,
            0.004M,
            0.005M
            );

        public override int init_level => 1;
    }
}
