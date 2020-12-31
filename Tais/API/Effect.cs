using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public enum EffectType
    {
        POP_TAX
    }

    public interface IEffect
    {
        decimal value { get; set; }
    }

    public abstract class PopTaxEffect : IEffect
    {
        public abstract decimal value { get; set; }
    }
}
