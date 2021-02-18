using Aritiafel.Artifacts.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TinvaValidatorTest
{
    [TestClass]
    public partial class CalculatorTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void OnePlusOneEuqalTwo()
        {
            LongConst a = new LongConst(1);
            LongConst b = new LongConst(1);
            TestContext.WriteLine((a + b).ToString());
        }

        [TestMethod]
        public void ConstPlusConst()
        {
            LongConst a = new LongConst(3);
            LongConst b = new LongConst(5);
            ArithmeticExpression ae = new ArithmeticExpression();
            ae.A = a;
            ae.B = b;
            ae.OP = Operator.Plus;
            
            TestContext.WriteLine(ae.GetResult(null).ToString());
        }

    }
}
