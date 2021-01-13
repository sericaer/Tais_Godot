using System;
using Tais.Visitor;

namespace Tais.API
{
    public class MethodGroup
    {
        public readonly static IVisitor<Type> INIT_PARTY = HelperClass<Init.Initer>.Property(x => x.party);
        public readonly static IVisitor<int> INIT_CHAOTING_TAX_LEVEL = HelperClass<Init.Initer>.Property(x => x.chaoting_tax_level);

        public readonly static IVisitor<int> YEAR = HelperClass<Run.Runner>.Property(x => x.date.value.y);
        public readonly static IVisitor<int> MONTH = HelperClass<Run.Runner>.Property(x => x.date.value.m);
        public readonly static IVisitor<int> DAY = HelperClass<Run.Runner>.Property(x => x.date.value.d);

        public static IDesc DESC(string _format, params object[] _objs)
        {
            return new Desc() { format = _format, objs = _objs };
        }

        public static T[] ARRAY<T>(params T[] array)
        {
            return array;
        }

        public static InitSelectOption OPTION_INIT_SELECT(IDesc _desc, IOperation[] ops, String next = null)
        {
            return new InitSelectOption() { desc = _desc, operations = ops, Next = next };
        }

        public static IOperation ASSIGN<T>(IVisitor<T> visitor, T value)
        {
            return new Mod.Operation.Assign<T>(visitor, value);
        }

        public static PopDef POP_NUM<T>(decimal num) where T : PopDef
        {
            var pop = Activator.CreateInstance(typeof(T)) as PopDef;
            pop.num = num;

            return pop;
        }

        public static String NEXT(Type type)
        {
            return type.FullName;
        }

        public static ConditionDef EQUAL<T>(IVisitor<T> left, T right)
        {
            return new Mod.Equal<T>(left, right);
        }
    }
}