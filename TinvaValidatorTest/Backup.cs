using Aritiafel.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TinvaValidatorTest
{
    [TestClass]
    public class Backup
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BackupProject()
        {
            string backupPath = @"E:\Backup";
            if (!Directory.Exists(backupPath))
                return;
            Residence rs = new Residence(backupPath);
            rs.SaveVSSolution(@"C:\Programs\Standard\TinaValidator", false);
            rs.SaveVSSolution(@"C:\Programs\Standard\Aritiafel", false);
        }
    }
}
