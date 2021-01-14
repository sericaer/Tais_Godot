namespace Tais.API
{
    public abstract class EventDef : MethodGroup
    {
        public abstract VaildDate date { get; }

        public virtual ConditionDef trigger => null;

        public abstract IDesc title { get; }
        public abstract IDesc desc { get; }

        public abstract OptionDef[] options { get; }

        public class VaildDate
        {
            public static VaildDate NULL = null;
            public static VaildDate ALL = new VaildDate() { };

            public int? year;
            public int? month;
            public int? day;
        }
    }
}
