using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Mod;
using Tais.Run;

namespace Tais
{
    class GMRoot
    {
        public static Runner runner;
        public static Modder modder;

        public static Action<object[]> logger
        {
            set
            {
                LOG.logger = value;
            }
        }
    }
}
