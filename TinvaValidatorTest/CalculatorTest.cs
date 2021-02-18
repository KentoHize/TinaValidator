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
        public void ConstCalculate()
        {
            LongConst a = new LongConst(3);
            LongConst b = new LongConst(5);
            DoubleConst c = new DoubleConst(7.5);
            ArithmeticExpression ae = new ArithmeticExpression(a, b);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, c, Operator.Multiply);

            TestContext.WriteLine(ae2.GetResult(null).ToString());
        }

        [TestMethod]
        public void VariableConst()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            DoubleVar d1 = new DoubleVar()
            LongConst a = new LongConst(3);
            LongConst b = new LongConst(5);
            DoubleConst c = new DoubleConst(7.5);
            ArithmeticExpression ae = new ArithmeticExpression(a, b);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, c, Operator.Multiply);

            TestContext.WriteLine(ae2.GetResult(null).ToString());
        }

    }
}
