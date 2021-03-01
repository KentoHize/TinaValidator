using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IUnit
    {
        ObjectConst Random(IVariableLinker variableLinker);
        bool Compare(ObjectConst b, IVariableLinker variableLinker);
    }
}
