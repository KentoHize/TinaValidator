namespace Aritiafel.Artifacts.Calculator
{
    public interface IBoolean : IObject
    {
        BooleanConst GetResult(IVariableLinker vl);
    }
}
