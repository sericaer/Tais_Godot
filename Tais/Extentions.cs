using ReactiveMarbles.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData.Binding;
using Tais.Run;

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

        public static IObservable<TReturn> OBSProperty<TObj, TReturn>(this TObj objectToMonitor,
            Expression<Func<TObj, TReturn>> propertyExpression)
            where TObj : class, INotifyPropertyChanged
        {
            return objectToMonitor.WhenPropertyValueChanges(propertyExpression);
        }

        public static bool Same(this Color left, (int r, int g, int b) right)
        {
            return left.r == right.r && left.g == right.g && left.b == right.b;
        }

        public static IObservable<IList<PropertyValue<TObject, TValue>>> ToOBSPropertyList<TObject, TValue>(this IEnumerable<TObject> source, Expression<Func<TObject, TValue>> propertyAccessor) where TObject : INotifyPropertyChanged
        {
            return Observable.CombineLatest(source.Select(x => x.WhenPropertyChanged(propertyAccessor, true)));
        }

        public static IntegrationImp<TS, TD> SetIntegration<TS, TD>(this Runner self, TS source, TD dest) where TS : class, INotifyPropertyChanged
        {
            IntegrationImp<TS, TD> integration = self.integrations.SingleOrDefault(x => x.GetType().GetGenericArguments()[0] == typeof(TS)
                                               && x.GetType().GetGenericArguments()[1] == typeof(TD)) as IntegrationImp<TS, TD>;
            if (integration == null)
            {
                integration = new IntegrationImp<TS, TD>(source, dest);
                self.integrations.Add(integration);
            }

            return integration;
        }

        public static IntegrationImp<TS, TD> SetIntegration<TS, TD>(this Runner self, TS source, IEnumerable<TD> dest) where TS : class, INotifyPropertyChanged
        {
            IntegrationImp<TS, TD> integration = self.integrations.SingleOrDefault(x => x.GetType().GetGenericArguments()[0] == typeof(TS)
                                               && x.GetType().GetGenericArguments()[1] == typeof(TD)) as IntegrationImp<TS, TD>;
            if (integration == null)
            {
                integration = new IntegrationImp<TS, TD>(source, dest);
                self.integrations.Add(integration);
            }

            return integration;
        }

        public static IntegrationImp<TS, TD> SetIntegration<TS, TD>(this Runner self, IEnumerable<TS> source, TD dest) where TS : class, INotifyPropertyChanged
        {
            IntegrationImp<TS, TD> integration = self.integrations.SingleOrDefault(x => x.GetType().GetGenericArguments()[0] == typeof(TS)
                                               && x.GetType().GetGenericArguments()[1] == typeof(TD)) as IntegrationImp<TS, TD>;
            if (integration == null)
            {
                integration = new IntegrationImp<TS, TD>(source, dest);
                self.integrations.Add(integration);
            }

            return integration;
        }
    }
}
