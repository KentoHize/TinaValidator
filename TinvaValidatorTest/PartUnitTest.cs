using Aritiafel;
using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TinvaValidatorTest
{
    [TestClass]
    public partial class PartUnitTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BooleanUnitTest()
        {
            BooleanUnit bu = new BooleanUnit();
            Assert.IsTrue(bu.Compare(BooleanConst.True));
            Assert.IsTrue(bu.Compare(BooleanConst.False));
            Assert.IsFalse(bu.Compare(new StringConst("aa")));
            Assert.IsFalse(bu.Compare(new LongConst(0)));
            Assert.IsTrue(bu.Compare(bu.Random()));
            Assert.IsTrue(bu.Compare(bu.Random()));
            TestContext.WriteLine(bu.Random().ToString());
            bu = BooleanUnit.True;
            Assert.IsTrue(bu.Compare(BooleanConst.True));
            Assert.IsFalse(bu.Compare(BooleanConst.False));
            Assert.IsTrue(bu.Compare(bu.Random()));            
            TestContext.WriteLine(bu.Random().ToString());
            bu = BooleanUnit.False;
            Assert.IsFalse(bu.Compare(BooleanConst.True));
            Assert.IsTrue(bu.Compare(BooleanConst.False));
            Assert.IsTrue(bu.Compare(bu.Random()));            
            TestContext.WriteLine(bu.Random().ToString());

            bu.CompareMethod = CompareMethod.Not;
            Assert.IsTrue(bu.Compare(BooleanConst.True));
            Assert.IsFalse(bu.Compare(BooleanConst.False));
            Assert.IsTrue(bu.Compare(bu.Random()));            
            TestContext.WriteLine(bu.Random().ToString());
        }

        [TestMethod]
        public void IntegerUnitTest()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            IntegerUnit iu = new IntegerUnit();
            Assert.IsTrue(iu.Compare(new LongConst(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1)));
            Assert.IsTrue(iu.Compare(new LongConst(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1)));
            Assert.IsTrue(iu.Compare(new LongConst(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1)));
            Assert.IsTrue(iu.Compare(LongConst.MinValue));
            Assert.IsTrue(iu.Compare(LongConst.MaxValue));
            Assert.IsFalse(iu.Compare(new StringConst("dd")));
            Assert.IsFalse(iu.Compare(new CharConst('\n')));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(3000);
            Assert.IsTrue(iu.Compare(new LongConst(3000)));
            Assert.IsFalse(iu.Compare(new LongConst(3001)));
            Assert.IsFalse(iu.Compare(new LongConst(2999))); 
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(-200, 200);
            Assert.IsFalse(iu.Compare(new LongConst(-300)));
            Assert.IsTrue(iu.Compare(new LongConst(-200)));
            Assert.IsTrue(iu.Compare(new LongConst(0)));
            Assert.IsTrue(iu.Compare(new LongConst(200)));
            Assert.IsFalse(iu.Compare(new DoubleConst(-100.3)));
            Assert.IsFalse(iu.Compare(new LongConst(500)));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(30, -68);
            Assert.ThrowsException<ArgumentException>(() => iu.Random());

            iu = new IntegerUnit(new long[] { 36, 57, 589435, 12368, 54867, 25728 });
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));

            iu.CompareMethod = CompareMethod.NotSelect;
            for(int i = 0; i < 300; i++)
                Assert.IsTrue(iu.Compare(iu.Random()));

            iu = IntegerUnit.UnsignedInt;
            for (int i = 0; i < 300; i++)
                Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());

            iu = new IntegerUnit(-9869, 87689986, CompareMethod.NotMinMax);
            decimal m;
            for(int i = 0; i < 5000; i++)
            {   
                Assert.IsTrue(iu.Compare(iu.Random()));
            }   
        }

        [TestMethod]
        public void DoubleUnitTest()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            DoubleUnit du = new DoubleUnit();
            Assert.IsTrue(du.Compare(new DoubleConst(rnd.NextRandomDouble())));
            Assert.IsTrue(du.Compare(new DoubleConst(rnd.NextRandomDouble())));
            Assert.IsTrue(du.Compare(new DoubleConst(rnd.NextRandomDouble())));
            Assert.IsTrue(du.Compare(new DoubleConst(rnd.NextRandomDouble())));
            Assert.IsTrue(du.Compare(new DoubleConst(rnd.NextRandomDouble())));
            Assert.IsTrue(du.Compare(DoubleConst.MinValue));
            Assert.IsTrue(du.Compare(DoubleConst.MaxValue));
            Assert.IsFalse(du.Compare(new StringConst("dd")));
            Assert.IsFalse(du.Compare(new CharConst('\n')));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            du = new DoubleUnit(56.6875);
            Assert.IsTrue(du.Compare(new DoubleConst(56.6875)));
            Assert.IsFalse(du.Compare(new DoubleConst(56.6876)));
            Assert.IsFalse(du.Compare(new DoubleConst(56.6874)));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            du = new DoubleUnit(-15.988, 360.62559);
            Assert.IsFalse(du.Compare(new LongConst(-30)));
            Assert.IsTrue(!du.Compare(new DoubleConst(-20.70368)));
            Assert.IsTrue(du.Compare(new DoubleConst(-15.98800)));
            Assert.IsTrue(du.Compare(new LongConst(0)));
            Assert.IsTrue(du.Compare(new DoubleConst(205.288)));
            Assert.IsTrue(du.Compare(new DoubleConst(360.62559)));
            Assert.IsFalse(du.Compare(new DoubleConst(360.62560)));
            Assert.IsFalse(du.Compare(new DoubleConst(578.698)));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            
            du = new DoubleUnit(6933.988, 5932.648819);
            Assert.ThrowsException<ArgumentException>(() => du.Random());

            du = new DoubleUnit(-965378515.5646, 8534832.683458, CompareMethod.NotMinMax);

            du = new DoubleUnit();
            for (int i = 0; i < 5000; i++)
            {
                DoubleConst d = du.Random() as DoubleConst;
                if (double.IsNaN((double)d.Value))
                    TestContext.Write($"{i}=NaN");
            }
                
        }

        [TestMethod]
        public void CharUnitTest()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            CharUnit cu = new CharUnit();
            Assert.IsTrue(cu.Compare(new CharConst((char)rnd.Next(char.MaxValue + 1))));
            Assert.IsTrue(cu.Compare(new CharConst((char)rnd.Next(char.MaxValue + 1))));
            Assert.IsTrue(cu.Compare(new CharConst((char)rnd.Next(char.MaxValue + 1))));
            Assert.IsTrue(cu.Compare(new CharConst((char)rnd.Next(char.MaxValue + 1))));
            Assert.IsTrue(cu.Compare(new CharConst((char)rnd.Next(char.MaxValue + 1))));
            Assert.IsTrue(cu.Compare(new CharConst(char.MinValue)));
            Assert.IsTrue(cu.Compare(new CharConst(char.MaxValue)));
            Assert.IsFalse(cu.Compare(new StringConst("dd")));
            Assert.IsFalse(cu.Compare(new LongConst(7)));
            Assert.IsTrue(cu.Compare(new CharConst('\a')));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));            
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('y');
            Assert.IsTrue(cu.Compare(new CharConst('y')));
            Assert.IsFalse(cu.Compare(new CharConst('z')));
            Assert.IsFalse(cu.Compare(new CharConst('x')));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('d', 'n');
            Assert.IsFalse(cu.Compare(new CharConst('a')));
            Assert.IsTrue(cu.Compare(new CharConst('d')));
            Assert.IsTrue(cu.Compare(new CharConst('f')));
            Assert.IsTrue(cu.Compare(new CharConst('k')));
            Assert.IsTrue(cu.Compare(new CharConst('n')));
            Assert.IsFalse(cu.Compare(new CharConst('o')));
            Assert.IsFalse(cu.Compare(new CharConst('z')));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            TestContext.WriteLine(cu.Random().ToString());

            cu = new CharUnit('z', 'r');
            Assert.ThrowsException<ArgumentException>(() => cu.Random());

            cu = new CharUnit('a', CompareMethod.Not);
            Assert.IsTrue(cu.Compare(new CharConst('d')));
            Assert.IsTrue(cu.Compare(new CharConst('f')));
            Assert.IsFalse(cu.Compare(new CharConst('a')));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));

            cu = new CharUnit('a', 'z', CompareMethod.NotMinMax);
            Assert.IsFalse(cu.Compare(new CharConst('d')));
            Assert.IsTrue(cu.Compare(new CharConst('A')));
            Assert.IsFalse(cu.Compare(new CharConst('a')));
            Assert.IsFalse(cu.Compare(new CharConst('z')));
            Assert.IsTrue(cu.Compare(new CharConst('-')));

            char c;
            for (int i = 0; i < 500; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu = new CharUnit("dhfejuteATR");            
            for (int i = 0; i < 1000; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu.CompareMethod = CompareMethod.NotSelect;
            for (int i = 0; i < 1000; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu = CharUnit.Letter;
            for (int i = 0; i < 1000; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu = CharUnit.Space;
            for (int i = 0; i < 300; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));
            
            cu = CharUnit.NotLetter;
            for (int i = 0; i < 3000; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu = CharUnit.NotSpace;
            for (int i = 0; i < 300; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            cu = CharUnit.NotDigit;
            for (int i = 0; i < 300; i++)
                Assert.IsTrue(cu.Compare(cu.Random()));

            //for (int i = 0; i < 300; i++)
            //{
            //    //TestContext.Write(cu.Random().ToString());
            //    TestContext.Write((Convert.ToInt32(cu.Random()).ToString() + ","));
            //    if (i % 50 == 0)
            //        TestContext.WriteLine("");
            //}
        }
    }
}
