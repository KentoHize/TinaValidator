﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanConst : ObjectConst, IBoolean
    {
        private bool _Value;
        public bool Value => _Value;
        public BooleanConst(bool value)
            => _Value = value;
        public static readonly BooleanConst True = new BooleanConst(true);
        public static readonly BooleanConst False = new BooleanConst(false);
        public BooleanConst GetResult(IVariableLinker vl)
            => this;
        public override string ToString()
            => _Value.ToString();
        public static bool operator true(BooleanConst a)
            => true;
        public static bool operator false(BooleanConst a)
            => false;
        public static BooleanConst operator &(BooleanConst a, BooleanConst b)
          => a.And(b);
        public static BooleanConst operator |(BooleanConst a, BooleanConst b)
          => a.Or(b);
        public static BooleanConst operator !(BooleanConst a)
          => a.Not();
        public BooleanConst And(BooleanConst b)
            => new BooleanConst(_Value && b._Value);
        public BooleanConst Or(BooleanConst b)
            => new BooleanConst(_Value || b._Value);
        public BooleanConst Not()
            => new BooleanConst(!_Value);
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IBoolean);

        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b is BooleanConst c ? c == this : throw new ArithmeticException();
        public override BooleanConst EqualTo(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst EqualTo(DoubleConst b)
            => throw new ArithmeticException();
        protected override BooleanConst ReverseGreaterThan(ObjectConst b) 
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(DoubleConst b)
            => throw new ArithmeticException();
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(LongConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(DoubleConst b)
            => throw new ArithmeticException();
    }
}
