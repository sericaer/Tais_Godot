using System;
using System.Linq.Expressions;
using Tais.Run;
using Tais.Visitor;

namespace Tais.API
{
    public class MethodGroup
    {
        public readonly static IVisitor<Type> INIT_PARTY = HelperClass<Init.Initer>.Property(x => x.party);
        public readonly static IVisitor<int> INIT_CHAOTING_TAX_LEVEL = HelperClass<Init.Initer>.Property(x => x.chaoting_tax_level);

        public readonly static IVisitor<bool> GM_END = HelperClass<Run.Runner>.Property(x => x.gmEnd);

        public readonly static IVisitorR<int> YEAR = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.date.value.y);
        public readonly static IVisitorR<int> MONTH = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.date.value.m);
        public readonly static IVisitorR<int> DAY = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.date.value.d);

        public readonly static IVisitorR<decimal> CHAOTING_EXPECT_YEAR_TAX = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.chaoting.expectYearTax);
        public readonly static IVisitor<decimal> CHAOTING_REPORT_YEAR_TAX = HelperClass<Run.Runner>.Property(x => x.chaoting.reportYearTax);
        public readonly static IVisitorR<decimal> CHAOTING_YEAR_TAX_OWE = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.chaoting.yearTaxOwe);
        public readonly static IVisitorR<decimal> CHAOTING_YEAR_TAX_EXCESS = HelperClass<Run.Runner>.PropertyOnlyRead(x => x.chaoting.yearTaxExcess);
        public readonly static IVisitor<decimal> CHAOTING_POWER = HelperClass<Run.Runner>.Property(x => x.chaoting.power);

        public readonly static IVisitor<int> LEVEL_REPORT_CHAOTING_TAX = HelperClass<Run.Runner>.Property(x => x.adjustReportChaotingTax.level);
        public readonly static IVisitor<int> MIN_LEVEL_REPORT_CHAOTING_TAX = HelperClass<Run.Runner>.Property(x => x.adjustReportChaotingTax.min_level);

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

        public static ConditionDef EQUAL<T>(IVisitorR<T> left, T right)
        {
            return new Mod.Condition.Equal<T>(left, right);
        }
        public static ConditionDef EQUAL<T>(IVisitorR<T> left, IVisitorR<T> right)
        {
            return new Mod.Condition.Equal<T>(left, right);
        }

        public static ConditionDef LESS(IVisitorR<decimal> left, decimal right)
        {
            return new Mod.Condition.Less(left, right);
        }

        public static ConditionDef LESS(IVisitorR<decimal> left, IVisitorR<decimal> right, Expression<Func<decimal, decimal>> Expression = null)
        {
            return new Mod.Condition.Less(left, right, Expression);
        }

        public static ConditionDef GREATER<T>(IVisitorR<T> left, T right) where T : IComparable
        {
            return new Mod.Condition.Greater<T>(left, right);
        }

        public static ConditionDef GREATER<T>(IVisitorR<T> left, IVisitorR<T> right, Expression<Func<T, T>> Expression = null) where T : IComparable
        {
            return new Mod.Condition.Greater<T>(left, right, Expression);
        }

        public static ConditionDef AND(ConditionDef left, ConditionDef right)
        {
            return new Mod.Condition.AndGroup(left, right);
        }

        public static EventDef.VaildDate VAILID_DATE(int? y, int? m, int? d)
        {
            return new EventDef.VaildDate() { year = y, month = m, day = d };
        }

        public static MathOp<decimal> ADD(decimal value)
        {
            return new MathOp<decimal>(value);
        }
    }
}