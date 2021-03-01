using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleConst : NumberConst
    {
        public static readonly DoubleConst MaxValue = new DoubleConst(double.MaxValue);

        public static readonly DoubleConst MinValue = new DoubleConst(double.MinValue);

        private double _Value;
        public override object Value => _Value;
        public DoubleConst(double value)
            => _Value = value;
        public static implicit operator double(DoubleConst a) => a._Value;
        public static explicit operator LongConst(DoubleConst a) => new LongConst(Convert.ToInt64(a._Value));
        public static NumberConst operator +(DoubleConst a, DoubleConst b)
            => a.Add(b);
        public static NumberConst operator +(DoubleConst a, LongConst b)
            => a.Add(b);
        public static NumberConst operator -(DoubleConst a, DoubleConst b)
            => a.Minus(b);
        public static NumberConst operator -(DoubleConst a, LongConst b)
            => a.Minus(b);
        public static NumberConst operator *(DoubleConst a, DoubleConst b)
            => a.Multiply(b);
        public static NumberConst operator *(DoubleConst a, LongConst b)
            => a.Multiply(b);
        public static NumberConst operator /(DoubleConst a, DoubleConst b)
            => a.Divide(b);
        public static NumberConst operator /(DoubleConst a, LongConst b)
            => a.Divide(b);
        public static NumberConst operator %(DoubleConst a, DoubleConst b)
            => a.Remainder(b);
        public static NumberConst operator %(DoubleConst a, LongConst b)
            => a.Remainder(b);
        public static BooleanConst operator ==(DoubleConst a, LongConst b)
            => a.EqualTo(b);
        public static BooleanConst operator ==(DoubleConst a, DoubleConst b)
            => a.EqualTo(b);
        public static BooleanConst operator !=(DoubleConst a, LongConst b)
            => !a.EqualTo(b);
        public static BooleanConst operator !=(DoubleConst a, DoubleConst b)
            => !a.EqualTo(b);
        public static BooleanConst operator >(DoubleConst a, LongConst b)
            => a.GreaterThan(b);
        public static BooleanConst operator >(DoubleConst a, DoubleConst b)
            => a.GreaterThan(b);
        public static BooleanConst operator >=(DoubleConst a, LongConst b)
            => a.GreaterThan(b) || a.EqualTo(b);
        public static BooleanConst operator >=(DoubleConst a, DoubleConst b)
            => a.GreaterThan(b) || a.EqualTo(b);
        public static BooleanConst operator <(DoubleConst a, LongConst b)
            => a.LessThan(b);
        public static BooleanConst operator <(DoubleConst a, DoubleConst b)
            => a.LessThan(b);
        public static BooleanConst operator <=(DoubleConst a, LongConst b)
            => a.LessThan(b) || a.EqualTo(b);
        public static BooleanConst operator <=(DoubleConst a, DoubleConst b)
            => a.LessThan(b) || a.EqualTo(b);
        public override string ToString()
            => _Value.ToString();
        public override StringConst ToStringConst()
            => new StringConst(_Value.ToString());
        public object GetValue()
            => _Value;
        public override NumberConst GetResult(IVariableLinker vl)
            => this;
        public override NumberConst Add(LongConst b)
            => LongAddDouble((long)b.Value, _Value);
        public override NumberConst Add(DoubleConst b)
            => new DoubleConst(_Value + b._Value);
        public override NumberConst Divide(LongConst b)
            => DoubleDivideLong(_Value, (long)b.Value);
        public override NumberConst Divide(DoubleConst b)
            => new DoubleConst(_Value / b._Value);
        public override NumberConst ExactlyDivide(LongConst b)
            => DoubleExactlyDivideLong(_Value, (long)b.Value);
        public override NumberConst ExactlyDivide(DoubleConst b)
            => new LongConst((long)(_Value / b._Value));
        public override NumberConst Minus(LongConst b)
            => DoubleMinusLong(_Value, (long)b.Value);
        public override NumberConst Minus(DoubleConst b)
            => new DoubleConst(_Value - b._Value);
        public override NumberConst Multiply(LongConst b)
            => LongMultiplyDouble((long)b.Value, _Value);
        public override NumberConst Multiply(DoubleConst b)
            => new DoubleConst(_Value * b._Value);
        public override NumberConst Remainder(LongConst b)
            => new DoubleConst(_Value % (double)b.Value); //0v0? Scan
        public override NumberConst Remainder(DoubleConst b)
            => new DoubleConst(_Value % b._Value);
        protected override NumberConst ReverseAdd(NumberConst b)
            => b.Add(this);
        protected override NumberConst ReverseMinus(NumberConst b)
            => b.Minus(this);
        protected override NumberConst ReverseMultiply(NumberConst b)
            => b.Multiply(this);
        protected override NumberConst ReverseDivide(NumberConst b)
            => b.Divide(this);
        public override NumberConst ReverseExactlyDivide(NumberConst b)
            => b.ExactlyDivide(this);
        protected override NumberConst ReverseRemainder(NumberConst b)
            => b.Remainder(this);
        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b is NumberConst ? b.EqualTo(this) : throw new ArithmeticException();
        public override BooleanConst EqualTo(DoubleConst b)
            => new BooleanConst(_Value == b._Value);
        public override BooleanConst EqualTo(LongConst b)
            => new BooleanConst(_Value == (long)b.Value);
        protected override BooleanConst ReverseGreaterThan(ObjectConst b)
            => b is NumberConst ? b.GreaterThan(this) : throw new ArithmeticException();
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => b is NumberConst ? b.LessThan(this) : throw new ArithmeticException();
        public override BooleanConst GreaterThan(DoubleConst b)
            => new BooleanConst(_Value > b._Value);
        public override BooleanConst GreaterThan(LongConst b)
            => new BooleanConst(_Value > (long)b.Value);
        public override BooleanConst LessThan(DoubleConst b)
            => new BooleanConst(_Value < b._Value);
        public override BooleanConst LessThan(LongConst b)
            => new BooleanConst(_Value < (long)b.Value);
        protected override BooleanConst ReverseEqualTo(NumberConst b)
            => b.EqualTo(this);
        protected override BooleanConst ReverseGreaterThan(NumberConst b)
            => b.GreaterThan(this);
        protected override BooleanConst ReverseLessThan(NumberConst b)
            => b.LessThan(this);
        public override BooleanConst EqualTo(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(StringConst b)
            => throw new ArithmeticException();
        public override NumberConst Add(CharConst b)
            => LongAddDouble((char)b.Value, _Value);
        public override NumberConst Minus(CharConst b)
            => DoubleMinusLong(_Value, (char)b.Value);
        public override NumberConst Multiply(CharConst b)
            => LongMultiplyDouble((char)b.Value, _Value);
        public override NumberConst Divide(CharConst b)
            => DoubleDivideLong(_Value, (char)b.Value);
        public override NumberConst ExactlyDivide(CharConst b)
            => DoubleExactlyDivideLong(_Value, (char)b.Value); // Scan
        public override NumberConst Remainder(CharConst b)
            => new DoubleConst(_Value % (char)b.Value);
        public override BooleanConst EqualTo(CharConst b)
            => new BooleanConst(_Value == (char)b.Value);
        public override BooleanConst GreaterThan(CharConst b)
            => new BooleanConst(_Value > (char)b.Value);
        public override BooleanConst LessThan(CharConst b)
            => new BooleanConst(_Value < (char)b.Value);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is NumberConst n))
                return false;
            return n.EqualTo(this);
        }
        public override int GetHashCode()
            => _Value.GetHashCode();

        public override object Clone()
            => new DoubleConst(_Value);
    }
}
