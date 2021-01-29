using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public abstract class WarnDef : MethodGroup
    {
        public abstract IDesc desc { get; }
        public abstract ConditionDef isTrigger { get; }
    }
}
