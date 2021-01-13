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

        [Fact]
        void LessTest()
        {
            var visitInt1 = Mock.Of<IVisitor<int>>(l => l.GetValue() == 1);


            var less1_0 = MethodGroup.LESS(visitInt1, 0);
            less1_0.isTrue().Should().BeFalse();

            var less1_1 = MethodGroup.LESS(visitInt1, 1);
            less1_1.isTrue().Should().BeFalse();

            var less1_2 = MethodGroup.LESS(visitInt1, 2);
            less1_2.isTrue().Should().BeTrue();
        }
    }
}
