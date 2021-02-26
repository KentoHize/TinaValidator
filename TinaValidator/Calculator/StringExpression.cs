using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringExpression : Expression, IString
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
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);

        public override object Clone()
            => new StringExpression((IString)A.Clone(), (IString)B.Clone(), OP);
    }
}
