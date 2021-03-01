using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IUnit
    {
        ObjectConst Random(IVariableLinker variableLinker = null);
        bool Compare(ObjectConst b, IVariableLinker variableLinker = null);
    }
}
