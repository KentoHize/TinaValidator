using System;
using System.Collections.Generic;
using System.Text;

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
            return ce.GetResult(VariableLinker).Value;
        }

        public bool CalculateBooleanExpression(BooleanExpression be)
        {
            return be.GetResult(VariableLinker).Value;
        }   

        public object CalculateArithmeticExpression(ArithmeticExpression ae)
        {   
            return ae.GetResult(VariableLinker).Value;
        }

        public string CalculateStringExpression()
        {
            return null;
        }
    }
}
