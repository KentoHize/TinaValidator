using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Unit : IUnit
    {
        public abstract bool Compare(ObjectConst b, IVariableLinker vl = null);
        public abstract ObjectConst Random(IVariableLinker vl = null);
    }
}
