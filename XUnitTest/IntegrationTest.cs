using System;
using System.IO;
using System.Linq;
using Tais;
using Tais.Mod;
using Xunit;

namespace XUnitTest
{
    public class IntegrationTestsFixture : IDisposable
    {
        public IntegrationTestsFixture()
        {
            GMRoot.modder = Modder.Load(Path.GetFullPath("../../../../TaisGodot/Release/Tais/mods/"));
            GMRoot.initer = new Tais.Init.Initer();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }
}
