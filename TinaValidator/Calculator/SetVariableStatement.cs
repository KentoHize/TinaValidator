namespace Aritiafel.Artifacts.Calculator
{
    public class SetVariableStatement : Statement
    {
        public Variable Variable { get; set; }
        public IObject Value { get; set; }
        public SetVariableStatement(Variable variable, IObject value = null)
        {
            Variable = variable;
            Value = value;
        }
    }
}
