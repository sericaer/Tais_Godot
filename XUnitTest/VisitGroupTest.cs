using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tais.API;
using Xunit;


namespace XUnitTest
{
    public class VisitGroupTest
    {
        [Fact]
        public void TestInitGroup()
        {
            MethodGroup.INIT_PARTY.ShouldBe<Tais.Init.Initer, Type>(x => x.party);
            MethodGroup.INIT_CHAOTING_TAX_LEVEL.ShouldBe<Tais.Init.Initer, int>(x => x.chaoting_tax_level);
        }
    }
}
