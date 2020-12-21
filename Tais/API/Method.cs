using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public static class Method
    {
        public static IDesc DESC(string _format, params string[] _objs)
        {
            return new Desc() { format = _format, objs = _objs };
        }

        public static T[] ARRAY<T>(params T[] array)
        {
            return array;
        }

        public static InitSelectOption OPTION_INIT_SELECT(IDesc desc)
        {
            return null;
        }

        public static Assign ASSIGN(IVisitor visitor, decimal value)
        {
            return new Assign(visitor, value);
        }
    }
}
