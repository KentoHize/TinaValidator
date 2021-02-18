using Aritiafel;
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
            Assert.IsTrue(bu.Compare(true));
            Assert.IsTrue(bu.Compare(false));
            Assert.IsTrue(!bu.Compare("aa"));
            Assert.IsTrue(!bu.Compare(0));
            TestContext.WriteLine(bu.Random().ToString());
            bu = new BooleanUnit(true);
            Assert.IsTrue(bu.Compare(true));
            Assert.IsTrue(!bu.Compare(false));
            TestContext.WriteLine(bu.Random().ToString());
            bu = new BooleanUnit(false);
            Assert.IsTrue(!bu.Compare(true));
            Assert.IsTrue(bu.Compare(false));
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
            Assert.IsTrue(!iu.Compare("dd"));
            Assert.IsTrue(!iu.Compare('\n'));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(3000);
            Assert.IsTrue(iu.Compare(3000));
            Assert.IsTrue(!iu.Compare(3001));
            Assert.IsTrue(!iu.Compare(2999));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(-200, 200);
            Assert.IsTrue(!iu.Compare(-300));
            Assert.IsTrue(iu.Compare(-200));
            Assert.IsTrue(iu.Compare(0));
            Assert.IsTrue(iu.Compare(200));
            Assert.IsTrue(!iu.Compare(-100.3));
            Assert.IsTrue(!iu.Compare(500));
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            TestContext.WriteLine(iu.Random().ToString());
            iu = new IntegerUnit(30, -68);
            Assert.ThrowsException<ArgumentException>(() => iu.Random());
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
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            du = new DoubleUnit(56.6875);
            Assert.IsTrue(du.Compare(56.6875));
            Assert.IsTrue(!du.Compare(56.6876));
            Assert.IsTrue(!du.Compare(56.6874));
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
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());
            TestContext.WriteLine(du.Random().ToString());

            du = new DoubleUnit(6933.988, 5932.648819);
            Assert.ThrowsException<ArgumentException>(() => du.Random());
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
            Assert.IsTrue(!cu.Compare("dd"));
            Assert.IsTrue(!cu.Compare(7));
            Assert.IsTrue(cu.Compare('\a'));
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('y');
            Assert.IsTrue(cu.Compare('y'));
            Assert.IsTrue(!cu.Compare('z'));
            Assert.IsTrue(!cu.Compare('x'));
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            cu = new CharUnit('d', 'n');
            Assert.IsTrue(!cu.Compare('a'));
            Assert.IsTrue(cu.Compare('d'));
            Assert.IsTrue(cu.Compare('f'));
            Assert.IsTrue(cu.Compare('k'));
            Assert.IsTrue(cu.Compare('n'));
            Assert.IsTrue(!cu.Compare('o'));
            Assert.IsTrue(!cu.Compare('z'));
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());
            TestContext.WriteLine(cu.Random().ToString());

            cu = new CharUnit('z', 'r');
            Assert.ThrowsException<ArgumentException>(() => cu.Random());
        }
    }
}
