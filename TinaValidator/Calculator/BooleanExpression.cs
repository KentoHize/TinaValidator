using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanExpression : Expression, IBoolean
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
                    return A.GetResult(vl) || B.GetResult(vl);
                case Operator.And:
                    return A.GetResult(vl) && B.GetResult(vl);
                case Operator.Not:
                    return !A.GetResult(vl);
                default:
                    throw new ArithmeticException();
            }
        }

        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IBoolean);
    }
}
