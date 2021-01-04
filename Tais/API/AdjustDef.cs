using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Run;

namespace Tais.API
{
    public abstract class AdjustDef
    {
        internal abstract ADJUST_TYPE type { get; } 

        public abstract int init_percent { get; }
    }

    public abstract class AdjustPopTaxDef : AdjustDef
    {
        internal override ADJUST_TYPE type => ADJUST_TYPE.POP_TAX;
    }

    public abstract class AdjustChaotingTaxDef : AdjustDef
    {
        internal override ADJUST_TYPE type => ADJUST_TYPE.CHAOTING_TAX;
    }
}
