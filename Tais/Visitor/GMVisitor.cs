using System;
using System.Reflection;
using Tais.API;

namespace Tais.Visitor
{
    class GMVisitor : IVisitor
    {
        public static object runner;
        public static object initer;

        public readonly Type rootType;
        public readonly PropertyInfo propertyInfo;

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
