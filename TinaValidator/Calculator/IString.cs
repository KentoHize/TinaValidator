namespace Aritiafel.Artifacts.Calculator
{
    public interface IString : IObject
    {
        StringConst GetResult(IVariableLinker vl);
    }
}
