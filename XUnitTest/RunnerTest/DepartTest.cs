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

namespace XUnitTest.RunnerTest
{
    public class DepartTest: IClassFixture<DepartTestFixture>
    {

        public static IDepartDef def;

        [Fact]
        public void InitTest()
        {
            var depart = new Depart(def);

            depart.name.Should().Be(def.GetType().FullName);
            depart.color.Should().Equals(new Color(1, 1, 1));

            depart.popNumDetail.Should().BeEquivalentTo(depart.pops.Select(x => (x.name, x.num)));
            depart.popNum.Should().Be(depart.popNumDetail.Sum(x=>x.value));


            depart.incomeDetail[IncomeDetail.TYPE.POP_TAX].Should().BeEquivalentTo(depart.pops.Select(pop => new Detail_Leaf(pop.name, pop.tax)));
            depart.tax.Should().Be(depart.incomeDetail.Sum());
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

        [Fact]
        public void TestSerialize()
        {
            var depart = new Depart(def);

            var json = JsonConvert.SerializeObject(depart,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var departDe = JsonConvert.DeserializeObject<Depart>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            departDe.color.Should().Equals(depart.color);
            departDe.popNum.Should().Be(depart.popNum);
            departDe.name.Should().Be(depart.name);

            int popNum = 0;
            IEnumerable<(string name, int num)> popNumDetail = null;
            depart.OBSProperty(x => x.popNum).Subscribe(x => popNum = x);
            depart.OBSProperty(x => x.popNumDetail).Subscribe(x => popNumDetail = x);

            decimal tax = 0M;
            IncomeDetail incomeDetail = null;
            depart.OBSProperty(x => x.tax).Subscribe(x => tax = x);
            depart.OBSProperty(x => x.incomeDetail).Subscribe(x => incomeDetail = x);

            depart.pops[0].num += 456;
            depart.pops[1].num -= 123;
            depart.pops[2].num -= 2000;

            popNumDetail.Should().BeEquivalentTo(depart.pops.Select(x => (x.name, x.num)));
            popNum.Should().Be(depart.popNumDetail.Sum(x => x.value));

            incomeDetail[IncomeDetail.TYPE.POP_TAX].Should().BeEquivalentTo(depart.pops.Select(pop => new Detail_Leaf(pop.name, pop.tax)));
            tax.Should().Be(depart.incomeDetail.Sum());
        }
    }

    public class DepartTestFixture : IDisposable
    {
        public DepartTestFixture()
        {
            var mock = new Mock<IDepartDef>();
            mock.Setup(l => l.pops).Returns(new IPopDef[] { new MockPop1(), new MockPop2(), new MockPop3() });
            mock.Setup(l => l.color).Returns((1, 1, 1));

            DepartTest.def = mock.Object;
        }

        public void Dispose()
        {
            
        }
    }

    public class MockPop1 : IPopDef
    {
        public bool is_tax => true;

        decimal IPopDef.num { get => 1000; set { } }
    }

    public class MockPop2 : IPopDef
    {
        public bool is_tax => true;
        public decimal num { get => 2000; set { } }
    }

    public class MockPop3 : IPopDef
    {
        public bool is_tax => false;
        public decimal num { get => 3000; set { } }
    }
}
