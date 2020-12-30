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
using System.ComponentModel;

namespace XUnitTest.RunnerTest
{
    public class DepartTest: IClassFixture<DepartTestFixture>
    {

        public static DepartDef def;

        [Fact]
        public void InitTest()
        {
            var depart = Depart.Gen(def);

            depart.name.Should().Be(def.GetType().FullName);
            depart.color.Should().Equals(new Color(1, 1, 1));

            VerifyOBservedIncome(depart);

            VerifyObservedPopNum(depart);
        }

        [Fact]
        public void PopNumChangedTest()
        {
            var depart = new Depart();
            depart.pops = new IPop[] { new MockPop() { isTax = true, num = 1000 }, new MockPop() { isTax = true, num = 2000 } };
            depart.IntegrateData();

            VerifyObservedPopNumChanged(depart, ()=>{
                depart.pops[0].num += 123;
                depart.pops[1].num -= 456;
            });
        }

        [Fact]
        public void PopTaxChangedTest()
        {
            var depart = new Depart();
            depart.pops = new IPop[] { new MockPop() { isTax = true, tax = 100 }, new MockPop() { isTax = true, tax = 200 } };
            depart.IntegrateData();

            VerifyObservedIncomeChanged(depart, ()=>
            {
                (depart.pops[0] as MockPop).tax += 12;
                (depart.pops[1] as MockPop).tax -= 45;
            });
        }

        [Fact]
        public void TestSerialize()
        {
            var depart = new Depart();
            depart.name = "TEST_DEPART";
            depart.color = new Color(1, 2, 4);
            depart.pops = new IPop[] { new MockPop() { isTax = true, num = 1000, tax = 100 }, new MockPop() { isTax = true, num = 2000, tax = 200 } };
            depart.IntegrateData();

            var json = JsonConvert.SerializeObject(depart,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var departDe = JsonConvert.DeserializeObject<Depart>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            departDe.color.Should().Equals(depart.color);
            departDe.popNum.Should().Be(depart.popNum);
            departDe.name.Should().Be(depart.name);

            VerifyObservedPopNumChanged(depart, () => {
                depart.pops[0].num += 456;
                depart.pops[1].num -= 789;
            });

            VerifyObservedIncomeChanged(depart, () =>
            {
                (depart.pops[0] as MockPop).tax += 45;
                (depart.pops[1] as MockPop).tax -= 89;
            });
        }


        private static void VerifyOBservedIncome(Depart depart)
        {
            decimal tax = 0;
            IncomeDetail incomeDetail = null;

            depart.OBSProperty(x => x.tax).Subscribe(x => tax = x);
            depart.OBSProperty(x => x.incomeDetail).Subscribe(x => incomeDetail = x);

            incomeDetail[IncomeDetail.TYPE.POP_TAX].Should().BeEquivalentTo(depart.pops.Select(pop => new Detail_Leaf(pop.name, pop.tax)));
            tax.Should().Be(depart.incomeDetail.value);
        }

        private static void VerifyObservedPopNum(Depart depart)
        {
            int popNum = 0;
            IEnumerable<(string name, int value)> popNumDetail = null;
            depart.OBSProperty(x => x.popNum).Subscribe(x => popNum = x);
            depart.OBSProperty(x => x.popNumDetail).Subscribe(x => popNumDetail = x);

            popNumDetail.Should().BeEquivalentTo(depart.pops.Select(x => (x.name, x.num)));
            popNum.Should().Be(popNumDetail.Sum(x => x.value));
        }

        private static void VerifyObservedIncomeChanged(Depart depart, Action act)
        {
            decimal tax = 0;
            IncomeDetail incomeDetail = null;

            depart.OBSProperty(x => x.tax).Subscribe(x => tax = x);
            depart.OBSProperty(x => x.incomeDetail).Subscribe(x => incomeDetail = x);

            act();

            incomeDetail[IncomeDetail.TYPE.POP_TAX].Should().BeEquivalentTo(depart.pops.Select(pop => new Detail_Leaf(pop.name, pop.tax)));
            tax.Should().Be(incomeDetail.value);
        }

        private static void VerifyObservedPopNumChanged(Depart depart, Action act)
        {
            int popNum = 0;
            IEnumerable<(string name, int value)> popNumDetail = null;
            depart.OBSProperty(x => x.popNum).Subscribe(x => popNum = x);
            depart.OBSProperty(x => x.popNumDetail).Subscribe(x => popNumDetail = x);

            act();

            popNumDetail.Should().BeEquivalentTo(depart.pops.Select(x => (x.name, x.num)));
            popNum.Should().Be(popNumDetail.Sum(x => x.value));
        }
    }

    public class DepartTestFixture : IDisposable
    {
        public DepartTestFixture()
        {
            var mock = new Mock<DepartDef>();
            mock.Setup(l => l.pops).Returns(new PopDef[] { new MockPop1Def(), new MockPop2Def(), new MockPop3Def() });
            mock.Setup(l => l.color).Returns((1, 1, 1));

            DepartTest.def = mock.Object;
        }

        public void Dispose()
        {
            
        }
    }

    public class MockPop1Def : PopDef
    {
        public override bool is_tax => true;

        public override decimal num { get => 1000; set { } }
    }

    public class MockPop2Def : PopDef
    {
        public override bool is_tax => true;
        public override decimal num { get => 2000; set { } }
    }

    public class MockPop3Def : PopDef
    {
        public override bool is_tax => false;
        public override decimal num { get => 3000; set { } }
    }

    public class MockPop : IPop
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get; set; }

        public bool isTax { get; set; }

        public decimal num { get; set; }

        public decimal tax { get; set; }

        public void UpdateTaxRate(decimal rate)
        {
            
        }
    }
}
