using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tais;
    using static Tais.VisitorGroup;

    namespace Native
    {
        public class PopH : IPop
        {
        }

        public class GEventTest : IEvent
        {
            public IOperation op => new Assign(GM_A, 999);
        }
    }
}
