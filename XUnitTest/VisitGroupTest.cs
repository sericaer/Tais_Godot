using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

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
}
