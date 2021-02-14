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
            Sequence se = new Sequence();
            Status st = new Status();
            TestContext.WriteLine(se.ToString());
        }
    }
}
