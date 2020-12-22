using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Tais.API;
using Tais.Visitor;
using Xunit;

using static Tais.API.VisitorGroup;

namespace XUnitTest
{
    public class VisitGroupTest
    {
        [Fact]
        public void TestInitGroup()
        {
            TestInitVisitor(INIT_PARTY, "party");
        }

        public void TestInitVisitor(IVisitor itf, string property)
        {
            TestVisitor(itf, typeof(Tais.Init.Initer), property);
        }

        public void TestVisitor(IVisitor itf, Type rootType, string property)
        {
            var visitor = itf as GMVisitor;
            visitor.rootType.Should().Be(rootType);
            visitor.propertyInfo.Name.Should().Be(property);
        }
    }
}
