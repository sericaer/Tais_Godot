using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Mod.Operation;

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

        public static InitSelectOption OPTION_INIT_SELECT(IDesc _desc, IOperation[] ops)
        {
            return new InitSelectOption() { desc = _desc, operations = ops };
        }

        public static Assign<T> ASSIGN<T>(IVisitor visitor, T value)
        {
            return new Assign<T>(visitor, value);
        }

        public static IPopDef POP_NUM<T>(decimal num) where T : IPopDef
        {
            var pop = Activator.CreateInstance(typeof(T)) as IPopDef;
            pop.num = num;

            return pop;
        }
    }
}
