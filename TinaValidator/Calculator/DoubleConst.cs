using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleConst : NumberConst
    {
        public override NumberConst Add(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Add(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Add(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Divide(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Divide(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Divide(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst ExactlyDivide(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst ExactlyDivide(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst ExactlyDivide(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst GetResult(IVariableLinker vl)
        {
            throw new NotImplementedException();
        }

        public override NumberConst RevrseMinus(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Minus(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Minus(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Multiply(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Multiply(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Multiply(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Remainder(NumberConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Remainder(LongConst b)
        {
            throw new NotImplementedException();
        }

        public override NumberConst Remainder(DoubleConst b)
        {
            throw new NotImplementedException();
        }

        public override object Value => _Value;

        private double _Value;
    }
}
