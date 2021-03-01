using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Unit : IUnit
    {
        public abstract bool Compare(object b, IVariableLinker vl);
        public abstract IObject Random(IVariableLinker vl);
    }
}
