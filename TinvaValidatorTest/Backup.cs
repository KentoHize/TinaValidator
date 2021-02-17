using Aritiafel.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            rs.SaveVSSolution(@"C:\Programs\Standard\TinaValidator", false);
            //rs.SaveVSSolution(@"C:\Programs\Standard\Aritiafel");
        }
    }
}
