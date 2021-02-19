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
        //"True"
        //"False"

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


        [TestMethod]
        public void BooleanExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            BooleanExpression be = new BooleanExpression(new BooleanConst(true), new BooleanConst(false));
            BooleanExpression be2 = new BooleanExpression(new BooleanVar(FakeVariableLinker.True), be, Operator.And);
            BooleanExpression be3 = new BooleanExpression(be2, null, Operator.Not);
            TestContext.WriteLine(be3.GetResult(fvl).ToString());
        }


        [TestMethod]
        public void CompareExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            CompareExpression ce = new CompareExpression(new LongConst(30), new LongConst(30));
            TestContext.WriteLine(ce.GetResult(fvl).ToString());
            CompareExpression ce2 = new CompareExpression(new LongConst(30), new LongVar(FakeVariableLinker.IntA));
            TestContext.WriteLine(ce2.GetResult(fvl).ToString());
            ArithmeticExpression ae = new ArithmeticExpression(new LongConst(30), new DoubleConst(30d));
            CompareExpression ce3 = new CompareExpression(ae, new LongVar(FakeVariableLinker.IntA));
            TestContext.WriteLine(ce3.GetResult(fvl).ToString());
            CompareExpression ce4 = new CompareExpression(new LongConst(40), new DoubleConst(30), Operator.GreaterThan);
            TestContext.WriteLine(ce4.GetResult(fvl).ToString());
        }
    }
}
