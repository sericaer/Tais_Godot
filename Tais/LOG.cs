using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais
{
    static class LOG
    {
        internal static Action<object[]> logger;

        public static void INFO(params object[] objs)
        {
            logger?.Invoke(objs);
        }
    }
}
