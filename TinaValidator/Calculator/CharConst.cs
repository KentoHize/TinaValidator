using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class CharConst : NumberConst
    {
        private char _Value;
        public override object Value => _Value;
        public CharConst(char value)
            => _Value = value;        
        public override StringConst ToStringConst()
            => new StringConst(_Value);

        public static implicit operator char(CharConst a) => a._Value;
        public override string ToString()
         => _Value.ToString();
        public override BooleanConst EqualTo(CharConst b)
            => new BooleanConst(_Value == b._Value);
        public override BooleanConst LessThan(CharConst b)
            => new BooleanConst(_Value < b._Value);
        public override BooleanConst GreaterThan(CharConst b)
            => new BooleanConst(_Value > b._Value);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is CharConst c))
                return false;
            return c.EqualTo(this);
        }
        public override int GetHashCode()
            => _Value.GetHashCode();
        public override object Clone()
            => new CharConst(_Value);

        protected override BooleanConst ReverseEqualTo(NumberConst b)
            => b.EqualTo(this);
        protected override BooleanConst ReverseGreaterThan(NumberConst b)
            => b.GreaterThan(this);
        protected override BooleanConst ReverseLessThan(NumberConst b)
            => b.LessThan(this);
        protected override NumberConst ReverseAdd(NumberConst b)
            => b.Add(this);
        public override NumberConst Add(LongConst b)
            => new LongConst(_Value + (long)b.Value);
        public override NumberConst Add(DoubleConst b)
            => new DoubleConst(_Value + (double)b.Value);
        public override NumberConst Add(CharConst b)
            => new LongConst(_Value + (int)b.Value);
        protected override NumberConst ReverseMinus(NumberConst b)
            => b.Minus(this);
        public override NumberConst Minus(LongConst b)
            => new LongConst(_Value - (long)b.Value);
        public override NumberConst Minus(DoubleConst b)
            => new DoubleConst(_Value - (double)b.Value);
        public override NumberConst Minus(CharConst b)
            => new DoubleConst(_Value - (int)b.Value);
        protected override NumberConst ReverseMultiply(NumberConst b)
            => b.Multiply(this);
        public override NumberConst Multiply(LongConst b)
            => new LongConst(_Value * (long)b.Value);
        public override NumberConst Multiply(DoubleConst b)
            => new DoubleConst(_Value * (double)b.Value);
        public override NumberConst Multiply(CharConst b)
            => new LongConst(_Value * (int)b.Value);
        protected override NumberConst ReverseDivide(NumberConst b)
            => b.Divide(this);
        public override NumberConst Divide(LongConst b)
            => new LongConst(_Value / (long)b.Value);
        public override NumberConst Divide(DoubleConst b)
            => new DoubleConst(_Value / (double)b.Value);
        public override NumberConst Divide(CharConst b)
            => new LongConst(_Value / (int)b.Value);
        public override NumberConst ReverseExactlyDivide(NumberConst b)
            => b.ExactlyDivide(this);
        public override NumberConst ExactlyDivide(LongConst b)
            => new LongConst(_Value / (long)b.Value);
        public override NumberConst ExactlyDivide(DoubleConst b)
            => LongExactlyDivideDouble(_Value, (double)b.Value);
        public override NumberConst ExactlyDivide(CharConst b)
            => new LongConst(_Value / (long)b.Value);
        protected override NumberConst ReverseRemainder(NumberConst b)
            => b.Remainder(this);
        public override NumberConst Remainder(LongConst b)
            => new LongConst(_Value % (long)b.Value);
        public override NumberConst Remainder(DoubleConst b)
            => new DoubleConst(_Value % (double)b.Value);
        public override NumberConst Remainder(CharConst b)
            => new LongConst(_Value % (long)b.Value);
        public override NumberConst GetResult(IVariableLinker vl)
            => this;
        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b.EqualTo(this);
        public override BooleanConst EqualTo(LongConst b)
            => new BooleanConst(_Value == (long)b.Value);
        public override BooleanConst EqualTo(DoubleConst b)
            => new BooleanConst(_Value == (double)b.Value);
        public override BooleanConst EqualTo(StringConst b)
            => new BooleanConst(b.Value.Length == 1 && b.Value[0] == _Value);
        protected override BooleanConst ReverseGreaterThan(ObjectConst b)
            => b.GreaterThan(this);
        public override BooleanConst GreaterThan(LongConst b)
            => new BooleanConst(_Value > (long)b.Value);
        public override BooleanConst GreaterThan(DoubleConst b)
            => new BooleanConst(_Value > (double)b.Value);
        public override BooleanConst GreaterThan(StringConst b)
            => new BooleanConst(b.Value.Length == 0 || b.Value.Length == 1 && _Value > b.Value[0]);
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => b.LessThan(this);
        public override BooleanConst LessThan(LongConst b)
            => new BooleanConst(_Value < (long)b.Value);
        public override BooleanConst LessThan(DoubleConst b)
            => new BooleanConst(_Value < (double)b.Value);
        public override BooleanConst LessThan(StringConst b)
            => new BooleanConst(b.Value.Length > 1 || b.Value.Length == 1 && _Value < b.Value[0]);
    }
}
