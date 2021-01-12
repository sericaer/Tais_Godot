namespace Tais.API
{
    public abstract class EventDef : MethodGroup
    {
        public abstract IDesc title { get; }
        public abstract IDesc desc { get; }

        public abstract OptionDef[] options { get; }
    }
}
