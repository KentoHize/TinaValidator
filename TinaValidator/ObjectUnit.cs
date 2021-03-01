using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ObjectUnit : Unit
    {
        public IObject Value { get; set; }
        public ObjectUnit(IObject value)
            => Value = value;

        public override bool Compare(object b, IVariableLinker vl)
            => Value == b;

        public override IObject Random(IVariableLinker vl)
            => Value;
    }
}
