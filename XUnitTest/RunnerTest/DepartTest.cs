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

namespace XUnitTest.RunnerTest
{
    public class DepartTest: IClassFixture<DepartTestFixture>
    {

        public static IDepart def;

        [Fact]
        public void InitTest()
        {
            var depart = new Depart(def);

            depart.color.Should().Equals(new Color(1, 1, 1));
            depart.popNum.Should().Be(def.pops.Where(x=>x.is_tax).Sum(x=>(int)x.num));
            depart.name.Should().Be(def.GetType().FullName);
        }

        [Fact]
        public void PopNumChangedTest()
        {
            var depart = new Depart(def);

            int rslt = 0;
            depart.OBSProperty(x => x.popNum).Subscribe(x => rslt = x);

            depart.pops[0].num += 123;
            depart.pops[1].num -= 456;
            depart.pops[2].num -= 2000;

            rslt.Should().Be(3000 + 123 - 456);
        }
    }

    public class DepartTestFixture : IDisposable
    {
        public DepartTestFixture()
        {
            var mock = new Mock<IDepart>();
            mock.Setup(l => l.pops).Returns(new IPop[] { new MockPop1(), new MockPop2(), new MockPop3() });
            mock.Setup(l => l.color).Returns((1, 1, 1));

            DepartTest.def = mock.Object;
        }

        public void Dispose()
        {
            
        }
    }

    public class MockPop1 : IPop
    {
        public bool is_tax => true;

        decimal IPop.num { get => 1000; set { } }
    }

    public class MockPop2 : IPop
    {
        public bool is_tax => true;
        public decimal num { get => 2000; set { } }
    }

    public class MockPop3 : IPop
    {
        public bool is_tax => false;
        public decimal num { get => 3000; set { } }
    }
}
