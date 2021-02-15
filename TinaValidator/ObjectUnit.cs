namespace Aritiafel.Artifacts.TinaValidator
{
    public class ObjectUnit : Unit, IUnit
    {
        public object Value { get; set; }

        public ObjectUnit(object value)
            => Value = value;

        public override bool Compare(object b)
            => this == b;

        public override object Random()
        {
            return new object();
        }
    }
}
