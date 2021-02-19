using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanExpression : IBoolean, IObject
    {
        public IBoolean A { get; set; }
        public IBoolean B { get; set; }
        public Operator OP { get; set; }

        public BooleanExpression(IBoolean a = null, IBoolean b = null, Operator op = Operator.Or)
        {
            A = a;
            B = b;
            OP = op;
        }

        public BooleanConst GetResult(IVariableLinker vl)
        {
            switch (OP)
            {
                case Operator.Or:
                    return A.GetResult(vl) | B.GetResult(vl);
                case Operator.And:
                    return A.GetResult(vl) & B.GetResult(vl);
                case Operator.Not:
                    return !A.GetResult(vl);
                default:
                    throw new ArithmeticException();
            }
        }

        public ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public Type GetObjectType()
            => typeof(IBoolean);
    }
}
