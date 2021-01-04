using System;
using Tais.Mod.Operation;
using Tais.Visitor;

namespace Tais.API
{
    public class MethodGroup
    {
        public readonly static IVisitor INIT_PARTY = HelperClass<Init.Initer>.Property(x => x.party);
        public readonly static IVisitor INIT_CHAOTING_TAX_LEVEL = HelperClass<Init.Initer>.Property(x => x.chaoting_tax_level);

        public static IDesc DESC(string _format, params string[] _objs)
        {
            return new Desc() { format = _format, objs = _objs };
        }

        public static T[] ARRAY<T>(params T[] array)
        {
            return array;
        }

        public static InitSelectOption OPTION_INIT_SELECT(IDesc _desc, IOperation[] ops, Type next = null)
        {
            return new InitSelectOption() { desc = _desc, operations = ops, Next = next };
        }

        public static Assign<T> ASSIGN<T>(IVisitor visitor, T value)
        {
            return new Assign<T>(visitor, value);
        }

        public static PopDef POP_NUM<T>(decimal num) where T : PopDef
        {
            var pop = Activator.CreateInstance(typeof(T)) as PopDef;
            pop.num = num;

            return pop;
        }

        public static Type NEXT(Type type)
        {
            return type;
        }
    }
}