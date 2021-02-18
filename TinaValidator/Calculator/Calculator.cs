using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class Calculator
    {
        IVariableLinker VariableLinker { get; set; }
        public void CalculateStatement()
        {

        }

        public bool CalculateBooleanExpression()
        {
            return false;
        }

        public object CalculateArithmeticExpression(ArithmeticExpression ae)
        {   
            return ae.GetResult(VariableLinker);
        }

        public string CalculateStringExpression()
        {
            return null;
        }

    }
}
