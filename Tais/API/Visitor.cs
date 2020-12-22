using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public static class VisitorGroup
    {
        public static IVisitor GM_A = new GMVisitor(typeof(Run.Runner), "a");

        public static IVisitor INIT_BACK_GROUND = new GMVisitor(typeof(Init.Initer), "background");
    }

    public interface IVisitor
    {
        void SetValue(object value);
    }

    class GMVisitor : IVisitor
    {
        public static object runner;
        public static object initer;

        private Type rootType;
        private PropertyInfo propertyInfo;
        public GMVisitor(Type rootType, string label)
        {
            this.rootType = rootType;
            this.propertyInfo = rootType.GetProperty(label) ;
        }

        public void SetValue(object value)
        {
            var obj = GetRootObject();
            propertyInfo.SetValue(obj, value);
        }

        private object GetRootObject()
        {
            if(rootType == typeof(Run.Runner))
            {
                return runner;
            }
            if (rootType == typeof(Init.Initer))
            {
                return initer;
            }

            throw new Exception();
        }
    }
}
