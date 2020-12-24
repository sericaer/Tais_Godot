using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Tais.API;
using Tais.Visitor;
using Xunit;
using System.Linq;

using static Tais.API.VisitorGroup;

namespace XUnitTest
{
    public class VisitGroupTest
    {
        [Fact]
        public void TestInitGroup()
        {
            INIT_PARTY.ShouldBe<Tais.Init.Initer>(x => x.party);
        }
    }

    public static class Extension
    {
        public static void ShouldBe<T>(this IVisitor itf, Expression<Func<T, dynamic>> expr)
        {
            var itf_expr = (IExpr)itf;
            itf_expr.lambda.ToString().Should().Be(expr.ToString());

            itf.GetType().GetGenericArguments().First().Should().Be(typeof(T));
        }
    }
}
