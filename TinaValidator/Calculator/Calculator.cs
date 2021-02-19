namespace Aritiafel.Artifacts.Calculator
{
    public class Calculator
    {
        IVariableLinker VariableLinker { get; set; }
        public void RunStatement()
        {

        }

        public bool CalculateCompareExpression(CompareExpression ce)
        {
            return ce.GetResult(VariableLinker);
        }

        public bool CalculateBooleanExpression(BooleanExpression be)
        {
            return be.GetResult(VariableLinker);
        }

        public object CalculateArithmeticExpression(ArithmeticExpression ae)
        {
            return ae.GetResult(VariableLinker).Value;
        }

        public string CalculateStringExpression(StringExpression se)
        {
            return se.GetResult(VariableLinker);
        }
    }
}
