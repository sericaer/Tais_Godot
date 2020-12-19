using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tais
{
    public static class VisitorGroup
    {
        public static IVisitor GM_A = new GMVisitor<decimal>(typeof(Run.Runner).GetProperty("a"));
    }

    public interface IVisitor
    {
        void SetValue(decimal value);
    }

    class GMVisitor<T> : IVisitor
    {
        private PropertyInfo propertyInfo;
        public GMVisitor(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        public void SetValue(decimal value)
        {
            propertyInfo.SetValue(GMRoot.runner, value);
        }
    }
}
