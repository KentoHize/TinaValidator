using System.Collections.Generic;
using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class Calculator
    {
        IVariableLinker VariableLinker { get; set; }

        public Calculator()
            => VariableLinker = new CalculatorMemory();
        
        public void Run(List<Statement> statements)
        {
            foreach (Statement st in statements)
                RunStatement(st);
        }
        public void RunStatement(Statement statement)
        {
            switch (statement)
            {
                case DeclareVariableStatement d:

                    break;
                default:
                    throw new NotImplementedException();
            }
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
