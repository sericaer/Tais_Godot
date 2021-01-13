namespace Tais.API
{
    public abstract class EventDef : MethodGroup
    {
        public abstract (int? y, int? m, int? d)? date { get; }

        public abstract ConditionDef trigger { get; }

        public abstract IDesc title { get; }
        public abstract IDesc desc { get; }

        public abstract OptionDef[] options { get; }
    }
}
