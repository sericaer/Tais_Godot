using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tais;
using Tais.API;
using Xunit;
using Tais.Run;
using FluentAssertions;
using Newtonsoft.Json;

namespace XUnitTest.ModTest
{
    public class ConditionTest
    {
        [Fact]
        void EqualTest()
        {
            var visitInt1 = Mock.Of<IVisitor<int>>(l => l.GetValue() == 1);

            var equal1_1 = MethodGroup.EQUAL(visitInt1, 1);
            equal1_1.isTrue().Should().BeTrue();

            var equal1_2 = MethodGroup.EQUAL(visitInt1, 2);
            equal1_2.isTrue().Should().BeFalse();
        }
    }
}
