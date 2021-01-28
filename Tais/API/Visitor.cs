using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.Visitor;

namespace Tais.API
{
    public interface IVisitor<T> : IVisitorR<T>
    {
        void SetValue(T value);
    }

    public interface IVisitorR<T>
    {
        T GetValue();
    }

    public class VisitorGroup
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

        public readonly static IVisitor<decimal> ECONOMY_VALUE = HelperClass<Run.Runner>.Property(x => x.economy.currValue);
        public readonly static IVisitor<int> ECONOMY_OWE_MONTH = HelperClass<Run.Runner>.Property(x => x.economy.oweMonths);

        public static object GetValue(string key)
        {
            try
            {
                var field = typeof(VisitorGroup).GetField(key, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                var fieldValue = field.GetValue(null);

                var GetValueMethod = fieldValue.GetType().GetMethod("GetValue");

                return GetValueMethod.Invoke(fieldValue, null);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string SetValue(string key, string value)
        {
            try
            {

                var field = typeof(VisitorGroup).GetField(key, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                var fieldValue = field.GetValue(null);

                var SetValueMethod = fieldValue.GetType().GetMethod("SetValue");

                var genericArgument = fieldValue.GetType().GetGenericArguments().ElementAt(1);
                var realValue = Convert.ChangeType(value, genericArgument);

                SetValueMethod.Invoke(fieldValue, new object[] { realValue });

                return "SET VALUE SUCCESS";
        }
            catch (Exception e)
            {
                return e.Message;
            }
}
    }
}
