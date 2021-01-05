using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using static Tais.API.Method;
namespace Native.Adjust
{
    public class ADJUST_POP_TAX : AdjustPopTaxDef
    {
        public override int init_percent => 10;
    }
}
