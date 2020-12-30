using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public abstract class AdjustDef
    {
        public abstract decimal[] level_rates { get; }

        public abstract int init_level { get; }
    }

    public abstract class AdjustTaxDef : AdjustDef
    {

    }
}
