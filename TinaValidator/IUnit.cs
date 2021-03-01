using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IUnit
    {
        object Random(IVariableLinker variableLinker);
        bool Compare(object b, IVariableLinker variableLinker);
    }
}
