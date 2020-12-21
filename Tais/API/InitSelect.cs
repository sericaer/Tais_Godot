using System;

namespace Tais.API
{
    public abstract class InitSelect
    {
        public IDesc title;
        public IDesc desc;
        public InitSelectOption[] options;
        public bool IsFirst;
    }

    public class InitSelectOption : IOption
    {
        public Type Next;
    }

    static class Extention_InitSelect
    {
        public static void CheckDefault(this InitSelect initSelect)
        {

        }
    }
}