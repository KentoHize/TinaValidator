using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestParse()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.InitialStatus = new Status();            
            UnitSet us = new UnitSet(CharUnits.AtoZ);
            us.Units.Add(CharUnits.atoz);
            VL.InitialStatus.Choices.Add(us);
            us.NextStatus = Status.EndStatus;            

            string testString = "DJ";

            TinaValidator tv = new TinaValidator(VL);
            bool result = tv.Validate(testString.Select(m => (object)m).ToArray());
            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(tv.CreateRandomToString());
        }

        [TestMethod]
        public void GetRangeEffective()
        {
            List<object> aList = new List<object>();
            for (int i = 0; i < 100000; i++)
                aList.Add(i);

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            aList.GetRange(5, aList.Count - 5);
            sw.Stop();
            TestContext.WriteLine(sw.ElapsedTicks.ToString());
        }

        [TestMethod]
        public void AreaParse()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.InitialStatus = new Status();
            
            Area ar1 = new Area(null, null, VL);
            ar1.InitialStatus = new Status();
            //12, 56 70 CHA
            //08, 32 45 CHR
            SkipPart sp = new SkipPart();
            VL.InitialStatus.Choices.Add(sp);
            sp.NextStatus = ar1.InitialStatus;
            CharsToIntegerPart stip = new CharsToIntegerPart();
            ar1.InitialStatus.Choices.Add(stip);
            stip.NextStatus = new Status();
            UnitSet us1 = new UnitSet(CharUnits.Comma);
            us1.Units.Add(CharUnits.WhiteSpace);
            stip.NextStatus.Choices.Add(us1);
            Status s3 = new Status();
            us1.NextStatus = s3;
            CharsToIntegerPart stip2 = new CharsToIntegerPart();
            s3.Choices.Add(stip2);
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
            Status s4 = new Status();
            stip2.NextStatus = s4;
            s4.Choices.Add(us2);
            Status s5 = new Status();
            us2.NextStatus = s5;
            CharsToIntegerPart stip3 = new CharsToIntegerPart();
            s5.Choices.Add(stip3);
            Status s6 = new Status();
            stip3.NextStatus = s6;
            UnitSet us3 = " CHA".ToUnitSet();
            s6.Choices.Add(us3);
            us3.NextStatus = Status.EndStatus;

            TinaValidator validator = new TinaValidator();
            validator.Logic = VL;

            bool result;
            using (FileStream fs = new FileStream(@"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File\A.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string s = sr.ReadToEnd();
                    List<object> thing = s.Select(m => (object)m).ToList();
                    result = validator.Validate(thing);
                }
            }

            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(validator.CreateRandomToString());
        }

        [TestMethod]
        public void SimpleParse()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.InitialStatus = new Status();
            CharsToIntegerPart stip = new CharsToIntegerPart();
            VL.InitialStatus.Choices.Add(stip);
            stip.NextStatus = new Status();            
            UnitSet us1 = new UnitSet(CharUnits.Comma);
            us1.Units.Add(CharUnits.WhiteSpace);
            stip.NextStatus.Choices.Add(us1);
            Status s3 = new Status();
            us1.NextStatus = s3;
            CharsToIntegerPart stip2 = new CharsToIntegerPart();
            s3.Choices.Add(stip2);
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
            Status s4 = new Status();
            stip2.NextStatus = s4;
            s4.Choices.Add(us2);
            Status s5 = new Status();
            us2.NextStatus = s5;
            CharsToIntegerPart stip3 = new CharsToIntegerPart();
            s5.Choices.Add(stip3);
            Status s6 = new Status();
            stip3.NextStatus = s6;
            UnitSet us3 = " CHA".ToUnitSet();
            s6.Choices.Add(us3);
            us3.NextStatus = Status.EndStatus;

            TinaValidator validator = new TinaValidator();
            validator.Logic = VL;

            bool result;
            using(FileStream fs = new FileStream(@"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File\A.txt", FileMode.Open))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    string s = sr.ReadToEnd();
                    List<object> thing = s.Select(m => (object)m).ToList();
                    result = validator.Validate(thing);
                }
            }

            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(validator.CreateRandomToString());
            //12, 56 70 CHA
            //08, 32 45 CHR

            
            for(int i = 0; i < 100; i++)
            {
                List<object> list = validator.CreateRandom();
                TestContext.WriteLine(list.ForEachToString());
                TestContext.WriteLine(validator.Validate(list).ToString());
            }
        }

        public void TestJsonConvert()
        {

        }
    }
}
