using System;
using System.Collections.Generic;
using System.Text;
using Aritiafel.Artifacts.Calculator;

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
                IntA => 60,
                IntB => -5,
                IntC => 999990,
                DoubleA => 2.55,
                DoubleB => 7.8,
                DoubleC => -65.237819,
                True => true,
                False => false,
                _ => null,
            };
        }

        public void SetValue(Variable v, object value)
        {
            throw new NotImplementedException();
        }
    }
}
