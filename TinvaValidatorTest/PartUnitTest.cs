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
            Assert.IsTrue(iu.Compare(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1));
            Assert.IsTrue(iu.Compare(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1));
            Assert.IsTrue(iu.Compare(rnd.Next() * rnd.Next(2) == 1 ? -1 : 1));
            Assert.IsTrue(iu.Compare(int.MinValue));
            Assert.IsTrue(iu.Compare(int.MaxValue));
            Assert.IsFalse(iu.Compare("dd"));
            Assert.IsFalse(iu.Compare('\n'));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(3000);
            Assert.IsTrue(iu.Compare(3000));
            Assert.IsFalse(iu.Compare(3001));
            Assert.IsFalse(iu.Compare(2999)); 
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            Assert.IsTrue(iu.Compare(iu.Random()));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(-200, 200);
            Assert.IsFalse(iu.Compare(-300));
            Assert.IsTrue(iu.Compare(-200));
            Assert.IsTrue(iu.Compare(0));
            Assert.IsTrue(iu.Compare(200));
            Assert.IsFalse(iu.Compare(-100.3));
            Assert.IsFalse(iu.Compare(500));
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

            iu = new IntegerUnit(new decimal[] { 36, 57, 589435, 12368, 54867, 25728 });
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
                //m = (decimal)iu.Random();
                Assert.IsTrue(iu.Compare(iu.Random()));
            }   
        }

        [TestMethod]
        public void DoubleUnitTest()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            DoubleUnit du = new DoubleUnit();
            Assert.IsTrue(du.Compare(rnd.NextRandomDouble()));
            Assert.IsTrue(du.Compare(rnd.NextRandomDouble()));
            Assert.IsTrue(du.Compare(rnd.NextRandomDouble()));
            Assert.IsTrue(du.Compare(rnd.NextRandomDouble()));
            Assert.IsTrue(du.Compare(rnd.NextRandomDouble()));
            Assert.IsTrue(du.Compare(double.MinValue));
            Assert.IsTrue(du.Compare(double.MaxValue));
            Assert.IsTrue(!du.Compare("dd"));
            Assert.IsTrue(!du.Compare('\n'));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            du = new DoubleUnit(56.6875);
            Assert.IsTrue(du.Compare(56.6875));
            Assert.IsTrue(!du.Compare(56.6876));
            Assert.IsTrue(!du.Compare(56.6874));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            Assert.IsTrue(du.Compare(du.Random()));
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            du = new DoubleUnit(-15.988, 360.62559);
            Assert.IsTrue(!du.Compare(-30));
            Assert.IsTrue(!du.Compare(-20.70368));
            Assert.IsTrue(du.Compare(-15.98800));
            Assert.IsTrue(du.Compare(0));
            Assert.IsTrue(du.Compare(205.288));
            Assert.IsTrue(du.Compare(360.62559));
            Assert.IsTrue(!du.Compare(360.62560));
            Assert.IsTrue(!du.Compare(578.698));
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
                double d = (double)du.Random();
                //if (d.ToString().Contains("NaN"))
                //    TestContext.Write($"{i}=\"{d.ToString()}\"");
                if (double.IsNaN(d))
                    TestContext.Write($"{i}=NaN");
                //Assert.IsTrue(du.Compare(du.Random()));
            }
                
        }

        [TestMethod]
        public void CharUnitTest()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            CharUnit cu = new CharUnit();
            Assert.IsTrue(cu.Compare((char)rnd.Next(char.MaxValue + 1)));
            Assert.IsTrue(cu.Compare((char)rnd.Next(char.MaxValue + 1)));
            Assert.IsTrue(cu.Compare((char)rnd.Next(char.MaxValue + 1)));
            Assert.IsTrue(cu.Compare((char)rnd.Next(char.MaxValue + 1)));
            Assert.IsTrue(cu.Compare((char)rnd.Next(char.MaxValue + 1)));
            Assert.IsTrue(cu.Compare(char.MinValue));
            Assert.IsTrue(cu.Compare(char.MaxValue));
            Assert.IsFalse(cu.Compare("dd"));
            Assert.IsFalse(cu.Compare(7));
            Assert.IsTrue(cu.Compare('\a'));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));            
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('y');
            Assert.IsTrue(cu.Compare('y'));
            Assert.IsFalse(cu.Compare('z'));
            Assert.IsFalse(cu.Compare('x'));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('d', 'n');
            Assert.IsFalse(cu.Compare('a'));
            Assert.IsTrue(cu.Compare('d'));
            Assert.IsTrue(cu.Compare('f'));
            Assert.IsTrue(cu.Compare('k'));
            Assert.IsTrue(cu.Compare('n'));
            Assert.IsFalse(cu.Compare('o'));
            Assert.IsFalse(cu.Compare('z'));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            TestContext.WriteLine(cu.Random().ToString());

            cu = new CharUnit('z', 'r');
            Assert.ThrowsException<ArgumentException>(() => cu.Random());

            cu = new CharUnit('a', CompareMethod.Not);
            Assert.IsTrue(cu.Compare('d'));
            Assert.IsTrue(cu.Compare('f'));
            Assert.IsFalse(cu.Compare('a'));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));
            Assert.IsTrue(cu.Compare(cu.Random()));

            cu = new CharUnit('a', 'z', CompareMethod.NotMinMax);
            Assert.IsFalse(cu.Compare('d'));
            Assert.IsTrue(cu.Compare('A'));
            Assert.IsFalse(cu.Compare('a'));
            Assert.IsFalse(cu.Compare('z'));
            Assert.IsTrue(cu.Compare('-'));

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
