using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aritiafel.Locations;

namespace TinvaValidatorTest
{
    [TestClass]
    public class Backup
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BackupProject()
        {
            Residence rs = new Residence(@"E:\Backup");
            rs.SaveVSSolution(@"C:\Programs\Standard\TinaValidator");
        }
    }
}
