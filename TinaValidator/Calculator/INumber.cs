namespace Aritiafel.Artifacts.Calculator
{
    public interface INumber : IObject
    {
        NumberConst GetResult(IVariableLinker vl);
    }
}
