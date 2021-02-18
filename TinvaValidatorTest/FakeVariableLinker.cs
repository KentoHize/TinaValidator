using System;
using System.Collections.Generic;
using System.Text;
using Aritiafel.Artifacts.Calculator;

namespace TinvaValidatorTest
{
    public class FakeVariableLinker : IVariableLinker
    {
        public object GetValue(Variable v)
        {
            switch(v.Name)
            {
                case "IntA":
                    return 60;
                case "IntB":
                    return -5;
                case "IntC":
                    return 999990;
                case "DoubleA":
                    return 2.55;
                case "DoubleB":
                    return 7.8;
                case "DoubleC":
                    return -65.237819;
                default:
                    return null;
            }
        }

        public void SetValue(Variable v, object value)
        {
            throw new NotImplementedException();
        }
    }
}
