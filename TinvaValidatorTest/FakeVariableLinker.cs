using Aritiafel.Artifacts.Calculator;
using System;

namespace TinvaValidatorTest
{
    public class FakeVariableLinker : IVariableLinker
    {
        public const string IntA = "IntA";
        public const string IntB = "IntB";
        public const string IntC = "IntC";
        public const string DoubleA = "DoubleA";
        public const string DoubleB = "DoubleB";
        public const string DoubleC = "DoubleC";
        public const string True = "True";
        public const string False = "False";
        public object GetValue(Variable v)
        {
            return v.Name switch
            {
                IntA => new LongConst(60),
                IntB => new LongConst(-5),
                IntC => new LongConst(999990),
                DoubleA => new DoubleConst(2.55),
                DoubleB => new DoubleConst(7.8),
                DoubleC => new DoubleConst(-65.237819),
                True => BooleanConst.True,
                False => BooleanConst.False,
                _ => null,
            };
        }

        public void SetValue(Variable v, IObject value)
        {
            throw new NotImplementedException();
        }
    }
}
