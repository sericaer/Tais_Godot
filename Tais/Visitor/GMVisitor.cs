using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tais.API;

namespace Tais.Visitor
{

    class GMVisitor<T, TProp> : VisitorData, IExpr, IVisitor<TProp>
    {
        public LambdaExpression lambda { get; set; }
     
        private Func<T, TProp> getter;
        private Action<T, TProp> setter;

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public GMVisitor(Expression<Func<T, TProp>> expr)
        {
            this.lambda = expr;

            this.setter = GenSetter();
            this.getter = expr.Compile();
        }

        public void SetValue(TProp value)
        {
            var obj = (T)dict[typeof(T)];
            setter(obj, value);
        }

        public TProp GetValue()
        {
            var obj = (T)dict[typeof(T)];
            return getter(obj);
        }

        private Action<T, TProp> GenSetter()
        {
            ParameterExpression valueParameterExpression = Expression.Parameter(typeof(TProp));
            Expression targetExpression = lambda.Body is UnaryExpression ? ((UnaryExpression)lambda.Body).Operand : lambda.Body;

            var newValue = Expression.Parameter(lambda.Body.Type);
            var assign = Expression.Lambda<Action<T, TProp>>
                        (
                            Expression.Assign(targetExpression, Expression.Convert(valueParameterExpression, targetExpression.Type)),
                            lambda.Parameters.Single(),
                            valueParameterExpression
                        );

            return assign.Compile();
        }
    }

    interface IExpr
    {
        LambdaExpression lambda { get; }
    }
    class VisitorData
    {
        public static Dictionary<Type, object> dict = new Dictionary<Type, object>();
    }

    static class HelperClass<T>
    {
        public static GMVisitor<T, TProp> Property<TProp>(Expression<Func<T, TProp>> expression)
        {
            return new GMVisitor<T, TProp>(expression);
        }
    }
}
