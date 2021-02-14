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

            TestContext.WriteLine(se.ToString());
        }
    }
}
