using FluentAssertions;
using System;
using System.Linq.Expressions;
using Tais.API;
using Tais.Visitor;
using System.Linq;
using Tais.Run;
using System.Collections.Generic;
using FluentAssertions.Collections;

namespace XUnitTest
{
    static class Extension
    {
        public static void ShouldBe<T>(this IVisitor self, Expression<Func<T, dynamic>> expr)
        {
            var self_expr = (IExpr)self;
            self_expr.lambda.ToString().Should().Be(expr.ToString());

            self.GetType().GetGenericArguments().First().Should().Be(typeof(T));
        }

        public static IntegrationTestElement<TS, TD> ContainSingle<TS, TD>(this GenericCollectionAssertions<Integration> self, TS source, TD dest)
        {
            var which = self.ContainSingle(x => x.srcObj.Equals(source) && x.destObj.Equals(dest)).Which;

            return new IntegrationTestElement<TS, TD>(which.binds);
        }
    }

    public class IntegrationTestElement<TS, TD>
    {
        private List<(LambdaExpression src, LambdaExpression dest)> binds;

        public IntegrationTestElement(List<(LambdaExpression src, LambdaExpression dest)> binds)
        {
            this.binds = binds;
        }

        public void IsBindWith<IProp>(Expression<Func<TS, IProp>> src, Expression<Func<TD, Action<IProp>>> dest)
        {
            binds.Should().ContainSingle(x => x.src.ToString() == src.ToString() && x.dest.ToString() == dest.ToString());
        }
    }
}
