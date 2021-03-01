using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IUnit
    {
        IObject Random(IVariableLinker variableLinker);
        bool Compare(object b, IVariableLinker variableLinker);
    }
}
