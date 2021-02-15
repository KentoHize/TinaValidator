using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aritiafel.Artifacts.TinaValidator;
namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            Thing th = new Thing();
            Status st1 = new Status();
            th.InitalStatus = st1;
            Sequence se = new Sequence();
            th.Choices.Add(se);
            Status st = new Status();
            TestContext.WriteLine(se.ToString());

            int a = 5;
            IntegerUnit IU = new IntegerUnit("dd", a);
        }
    }
}
