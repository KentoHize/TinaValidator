using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringConst : ObjectConst, IString
    {
        private string _Value;
        public string Value => _Value;
        public CharConst this[int index] => new CharConst(_Value[index]);
        public StringConst(string value)
            => _Value = value;
        public StringConst(char value)
            => _Value = value.ToString();
        public override StringConst ToStringConst()
            => this;

        public static implicit operator string(StringConst a) => a._Value;
        public override string ToString()
         => _Value.ToString();
        public static BooleanConst operator ==(StringConst a, StringConst b)
           => a.EqualTo(b);
        public static BooleanConst operator !=(StringConst a, StringConst b)
            => !a.EqualTo(b);
        public static StringConst operator +(StringConst a, StringConst b)
            => a.Concat(b);
        public StringConst Concat(StringConst b)
            => new StringConst(string.Concat(_Value, b._Value));
        public override BooleanConst EqualTo(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(StringConst b)
            => new BooleanConst(_Value == b._Value);
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);
        public StringConst GetResult(IVariableLinker vl)
            => this;
        public override BooleanConst GreaterThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(StringConst b)
            => new BooleanConst(_Value.CompareTo(b._Value) == 1);
        public override BooleanConst LessThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(DoubleConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(StringConst b)
            => new BooleanConst(_Value.CompareTo(b._Value) == -1);
        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b.EqualTo(this);
        protected override BooleanConst ReverseGreaterThan(ObjectConst b)
            => b.GreaterThan(this);
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => b.LessThan(this);
        public override BooleanConst EqualTo(CharConst b)
            => new BooleanConst(_Value.Length == 1 && _Value[0] == (char)b.Value);
        public override BooleanConst GreaterThan(CharConst b)
            => new BooleanConst(_Value.Length > 1 || _Value.Length == 1 && _Value[0] > (char)b.Value);
        public override BooleanConst LessThan(CharConst b)
            => new BooleanConst(_Value.Length < 1 || _Value.Length == 1 && _Value[0] < (char)b.Value);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is StringConst s))
                return false;
            return s.EqualTo(this);
        }
        public override int GetHashCode()
            => _Value.GetHashCode();
        public override object Clone()
            => new StringConst(_Value);
    }
}
