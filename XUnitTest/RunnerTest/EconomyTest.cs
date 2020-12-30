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
    public class EconomyTest
    {

        [Fact]
        void TestUpdateIncome()
        {
            Economy economy = new Economy();

            var incomeDetail1 = new IncomeDetail("INCOME_1");
            economy.UpdateIncome(incomeDetail1);

            economy.incomes.Count().Should().Be(1);
            economy.incomes.Should().Contain(incomeDetail1);

            var incomeDetail1_2 = new IncomeDetail("INCOME_1");
            economy.UpdateIncome(incomeDetail1_2);

            economy.incomes.Count().Should().Be(1);
            economy.incomes.Should().Contain(incomeDetail1_2);

            var incomeDetail2 = new IncomeDetail("INCOME_2");
            economy.UpdateIncome(incomeDetail2);

            economy.incomes.Count().Should().Be(2);
            economy.incomes.Should().Contain(incomeDetail1_2);
            economy.incomes.Should().Contain(incomeDetail2);
        }

        [Fact]
        void TestDaysInc()
        {
            Economy economy = new Economy();

            decimal currValue = -1;
            economy.OBSProperty(x => x.currValue).Subscribe(x => currValue = x);

            var incomeDetail1 = new IncomeDetail("INCOME_1");
            incomeDetail1.Update(IncomeDetail.TYPE.POP_TAX, new Detail_Leaf[] { new Detail_Leaf("LEAF1", 3) });

            economy.UpdateIncome(incomeDetail1);

            int count = 1;
            for(int y=1; y<11; y++)
            {
                for(int m=1; m<13; m++)
                {
                    for(int d=1; d<31; d++)
                    {
                        economy.DaysInc((y, m, d));

                        if(d == 30)
                        {
                            currValue.Should().Be(count++ * incomeDetail1.value);
                        }
                    }
                }
            }
        }
    }
}
