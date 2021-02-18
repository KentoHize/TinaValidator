﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleConst : NumberConst
    {
        private double _Value;
        public override object Value => _Value;
        public DoubleConst(double value)
            => _Value = value;
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
        public override string ToString()
            => _Value.ToString();
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
            => new DoubleConst(_Value % (double)b.Value); //0v0?
        public override NumberConst Remainder(DoubleConst b)
            => new DoubleConst(_Value * b._Value);
        public override NumberConst ReverseAdd(NumberConst b)
            => b.Add(this);
        public override NumberConst ReverseMinus(NumberConst b)
            => b.Minus(this);
        public override NumberConst ReverseMultiply(NumberConst b)
            => b.Multiply(this);
        public override NumberConst ReverseDivide(NumberConst b)
            => b.Divide(this);
        public override NumberConst ReverseExactlyDivide(NumberConst b)
            => b.ExactlyDivide(this);
        public override NumberConst ReverseRemainder(NumberConst b)
            => b.Remainder(this);
    }
}