using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanConst : ObjectConst, IBoolean
    {
        private bool _Value;
        public bool Value => _Value;
        public BooleanConst(bool value)
            => _Value = value;
        public static BooleanConst True => new BooleanConst(true);
        public static BooleanConst False => new BooleanConst(false);
        public BooleanConst GetResult(IVariableLinker vl)
            => this;
        public override string ToString()
            => _Value.ToString();
        public static bool operator true(BooleanConst a)
            => a._Value;
        public static bool operator false(BooleanConst a)
            => !a._Value;
        public static BooleanConst operator &(BooleanConst a, BooleanConst b)
          => a.And(b);
        public static BooleanConst operator |(BooleanConst a, BooleanConst b)
          => a.Or(b);
        public static BooleanConst operator ^(BooleanConst a, BooleanConst b)
          => a.XOr(b);
        public static BooleanConst operator !(BooleanConst a)
          => a.Not();
        public static BooleanConst operator ==(BooleanConst a, BooleanConst b)
          => new BooleanConst(a._Value == b._Value);
        public static BooleanConst operator !=(BooleanConst a, BooleanConst b)
          => new BooleanConst(a._Value != b._Value);

        public static implicit operator bool(BooleanConst a) => a._Value;
        public BooleanConst And(BooleanConst b)
            => new BooleanConst(_Value && b._Value);
        public BooleanConst Or(BooleanConst b)
            => new BooleanConst(_Value || b._Value);
        public BooleanConst XOr(BooleanConst b)
            => new BooleanConst(_Value ^ b.Value);
        public BooleanConst Not()
            => new BooleanConst(!_Value);
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override StringConst ToStringConst()
            => new StringConst(_Value.ToString());
        public override Type GetObjectType()
            => typeof(IBoolean);
        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b is BooleanConst c ? c == this : throw new ArithmeticException();
        public override BooleanConst EqualTo(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(CharConst c)
            => throw new ArithmeticException();
        protected override BooleanConst ReverseGreaterThan(ObjectConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(CharConst b)
            => throw new ArithmeticException();        
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(CharConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(StringConst b)
            => throw new ArithmeticException();
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is BooleanConst n))
                return false;
            return n._Value == _Value;
        }
        public override int GetHashCode()
            => _Value.GetHashCode();
        public override object Clone()
            => new BooleanConst(_Value);
    }
}
