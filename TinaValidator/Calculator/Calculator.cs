using System.Collections.Generic;
using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class Calculator
    {
        CalculatorMemory VariableLinker { get; set; }
        public List<Statement> Statements { get; set; }
        public Calculator()
            : this(null)
        { }

        public Calculator(List<Statement> statements)
        {
            VariableLinker = new CalculatorMemory();
            Statements = statements ?? new List<Statement>();
        }

        public void Run()
            => RunStatements(Statements);
        
        public void RunStatements(List<Statement> statements)
        {
            foreach (Statement st in statements)
                RunStatement(st);
        }
        public void RunStatement(Statement statement)
        {
            switch (statement)
            {
                case DeclareVariableStatement dvs:
                    VariableLinker.DeclareVariable(dvs.Name, dvs.Type, dvs.Dimension, dvs.Counts, dvs.InitialValue);
                    break;
                case SetVariableStatement svs:
                    VariableLinker.SetValue(svs.Variable, svs.Value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool CalculateCompareExpression(IBoolean ce)
            => ce.GetResult(VariableLinker);
        public bool CalculateBooleanExpression(IBoolean be)
            => be.GetResult(VariableLinker);
        public object CalculateArithmeticExpression(INumber ae)
            => ae.GetResult(VariableLinker).Value;
        public string CalculateStringExpression(IString se)
            => se.GetResult(VariableLinker);
        public void ClearMemory()
            => VariableLinker.CleaerAllVariables();
    }
}
