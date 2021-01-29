using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Native.Warn
{
    public class WARN_ECONOMY_IS_MINUS : WARN
    {
        public override IDesc desc => DESC("WARN_ECONOMY_IS_MINUS_DESC");

        public override ConditionDef isTrigger => LESS(ECONOMY_VALUE, 0);
    }
}
