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

        //"IntA": 60;
        //"IntB": -5;
        //"IntC": 999990;
        //"DoubleA": 2.55;
        //"DoubleB": 7.8;
        //"DoubleC": -65.237819;

        [TestMethod]
        public void VariableAndConst()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            DoubleVar d1 = new DoubleVar(FakeVariableLinker.DoubleA);
            LongVar i1 = new LongVar(FakeVariableLinker.IntA);
            LongConst a = new LongConst(300);
            DoubleConst b = new DoubleConst(20.7);
            ArithmeticExpression ae = new ArithmeticExpression(a, i1);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, d1, Operator.Multiply);
            ArithmeticExpression ae3 = new ArithmeticExpression(ae2, b, Operator.Minus);

            TestContext.WriteLine(ae3.GetResult(fvl).ToString());
        }
    }
}
