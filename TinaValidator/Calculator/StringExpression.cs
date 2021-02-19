using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringExpression : IString, IObject
    {
        public IString A { get; set; }
        public IString B { get; set; }
        public Operator OP { get; set; }

        public StringExpression(IString a = null, IString b = null, Operator op = Operator.Concat)
        {
            A = a;
            B = b;
            OP = op;
        }
        public StringConst GetResult(IVariableLinker vl)
        {
            switch (OP)
            {
                case Operator.Concat:
                    return A.GetResult(vl) + B.GetResult(vl);
                default:
                    throw new ArithmeticException();
            }
        }
        public ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public Type GetObjectType()
            => typeof(IString);
    }
}
