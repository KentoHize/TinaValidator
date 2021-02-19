using Aritiafel.Artifacts.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json;

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
            Assert.IsTrue(ae3.GetResult(fvl).ToString() == "897.3");
        }


        [TestMethod]
        public void BooleanExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            BooleanExpression be = new BooleanExpression(BooleanConst.True, BooleanConst.False);
            BooleanExpression be2 = new BooleanExpression(new BooleanVar(FakeVariableLinker.True), be, Operator.And);
            BooleanExpression be3 = new BooleanExpression(be2, null, Operator.Not);
            Assert.IsFalse(be3.GetResult(fvl).Value);
        }


        [TestMethod]
        public void CompareExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            LongConst a = new LongConst(0304);
            NumberConst b = new DoubleConst(56.8);
            ObjectConst c = new DoubleConst(690.8);
            Assert.IsFalse((a < b).Value);
            Assert.IsTrue((a < c).Value);
            CompareExpression ce = new CompareExpression(new LongConst(30), new LongConst(30));
            Assert.IsTrue(ce.GetResult(fvl).Value);
            CompareExpression ce2 = new CompareExpression(new LongConst(30), new LongVar(FakeVariableLinker.IntA));
            Assert.IsFalse(ce2.GetResult(fvl).Value);
            ArithmeticExpression ae = new ArithmeticExpression(new LongConst(30), new DoubleConst(30d));
            CompareExpression ce3 = new CompareExpression(ae, new LongVar(FakeVariableLinker.IntA));
            Assert.IsTrue(ce3.GetResult(fvl).Value);
            CompareExpression ce4 = new CompareExpression(new LongConst(40), new DoubleConst(30), Operator.GreaterThan);
            Assert.IsTrue(ce4.GetResult(fvl).Value);
            CompareExpression ce5 = new CompareExpression(new BooleanConst(true), new DoubleConst(30), Operator.GreaterThan);
            Assert.ThrowsException<ArithmeticException>(() => ce5.GetResult(fvl));
            CompareExpression ce6 = new CompareExpression(new LongConst(20), new DoubleConst(30), Operator.LessThan);
            Assert.IsTrue(ce6.GetResult(fvl).Value);
            CompareExpression ce7 = new CompareExpression(new LongConst(20), new DoubleConst(20), Operator.LessThanOrEqualTo);
            Assert.IsTrue(ce7.GetResult(fvl).Value);
            CompareExpression ce8 = new CompareExpression(new DoubleVar(FakeVariableLinker.DoubleA), new DoubleConst(2.55), Operator.NotEqualTo);
            Assert.IsFalse(ce8.GetResult(fvl).Value);
        }

        [TestMethod]
        public void StringExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();
            StringExpression se = new StringExpression(new StringConst('a'), new StringConst("bbbb"));
            Assert.IsTrue(se.GetResult(fvl).Value == "abbbb");
            StringExpression se2 = new StringExpression(new StringConst("add"), new StringConst("__bb"));
            Assert.IsTrue(se2.GetResult(fvl).Value == "add__bb");
            StringExpression se3 = new StringExpression(new DoubleVar(FakeVariableLinker.DoubleB).GetObject(fvl).ToStringConst(), new StringConst("bb"));
            Assert.IsTrue(se3.GetResult(fvl).Value == "7.8bb");            
        }

        [TestMethod]
        public void JsonSave()
        {
            //JsonConvert.SerializeObject()
        }
    }
}
