using System;

namespace Tais.API
{
    public abstract class InitSelect : MethodGroup
    {
        public abstract IDesc title { get; }
        public abstract IDesc desc { get; }
        public abstract InitSelectOption[] options { get; }
        public virtual bool IsFirst => false;
    }

    public class InitSelectOption : IOption
    {
        public String Next;
    }

    static class Extention_InitSelect
    {
        public static void CheckDefault(this InitSelect initSelect)
        {

        }
    }
}