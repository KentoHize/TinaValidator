using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongConst : NumberConst
    {
        public static NumberConst operator+(LongConst a, LongConst b)
            => new LongConst(a.Value + b.Value);
        public override string ToString()
            => Value.ToString();

        public object GetValue()
            => Value;

        public override NumberConst GetResult(IVariableLinker vl)
            => this;        

        public NumberConst Add(LongConst b)
            => new LongConst(3);

        public override NumberConst Add(NumberConst b)
        {
            
        }

        public override NumberConst Minus(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Multiply(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Divide(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Remainder(NumberConst b)
        {
            throw new NotImplementedException();
        }

        //public override NumberConst ReverseMinus(NumberConst b)
        //{
        //    return b.Minus(this);
        //}

        public override NumberConst ReverseDivide(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst ReverseRemainder(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst ReverseMinus(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public LongConst(long value)
        {
            Value = value;
        }
        private long Value { get; set; }
    }
}
