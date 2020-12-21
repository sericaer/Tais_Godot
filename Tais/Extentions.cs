using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais
{
    static class Extentions
    {
        public static void Do (this InitSelectOption source)
        {
            if(source.operations == null)
            {
                return;
            }

            foreach(var op in source.operations)
            {
                op.Do();
            }
        }

        public static InitSelect GetNext(this InitSelectOption source)
        {
            if(source.Next == null)
            {
                return null;
            }

            return Activator.CreateInstance(source.Next) as InitSelect;
        }
    }
}
