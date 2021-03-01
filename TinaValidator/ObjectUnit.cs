using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ObjectUnit : Unit
    {
        public IObject Value { get; set; }
        public ObjectUnit(IObject value)
            => Value = value;

        public override bool Compare(ObjectConst b, IVariableLinker vl)
            => Value == b;

        public override ObjectConst Random(IVariableLinker vl)
            => Value;
    }
}
