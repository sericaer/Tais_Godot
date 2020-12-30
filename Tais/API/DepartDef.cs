using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public abstract class DepartDef
    {
        public abstract Color color { get; }
        public abstract PopDef[] pops { get; }
    }
}
