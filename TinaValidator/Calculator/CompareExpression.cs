using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class CompareExpression : Expression, IBoolean
    {
        public IObject A { get; set; }
        public IObject B { get; set; }
        public Operator OP { get; set; }

        public CompareExpression(IObject a = null, IObject b = null, Operator op = Operator.EqualTo)
        {
            A = a;
            B = b;
            OP = op;
        }
        public BooleanConst GetResult(IVariableLinker vl)
        {
            switch (OP)
            {
                case Operator.EqualTo:
                    return A.GetObject(vl) == B.GetObject(vl);
                case Operator.NotEqualTo:
                    return A.GetObject(vl) != B.GetObject(vl);
                case Operator.GreaterThan:
                    return A.GetObject(vl) > B.GetObject(vl);
                case Operator.GreaterThanOrEqualTo:
                    return A.GetObject(vl) >= B.GetObject(vl);
                case Operator.LessThan:
                    return A.GetObject(vl) < B.GetObject(vl);
                case Operator.LessThanOrEqualTo:
                    return A.GetObject(vl) <= B.GetObject(vl);
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