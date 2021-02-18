using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class ArithmeticExpression : Number
    {
        public Number A { get; set; }
        public Number B { get; set; }        
        public Operator OP { get; set; }

        public override NumberConst GetResult(IVariableLinker vl)
        {
            switch(OP)
            {
                case Operator.Plus:
                    return A.GetResult(vl) + B.GetResult(vl);
                case Operator.Minus:
                    return A.GetResult(vl) - B.GetResult(vl);
                case Operator.Multiply:
                    return A.GetResult(vl) * B.GetResult(vl);
                case Operator.Divide:
                    return A.GetResult(vl) / B.GetResult(vl);
                case Operator.Remainder:
                    return A.GetResult(vl) % B.GetResult(vl);
                case Operator.PlusOne:
                    return A.GetResult(vl) + new LongConst(1);
                case Operator.MinusOne:
                    return A.GetResult(vl) - new LongConst(1);
                default:
                    throw new ArithmeticException();
            }
        }
    }
}
