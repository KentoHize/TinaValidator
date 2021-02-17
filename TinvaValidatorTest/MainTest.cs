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
        public void TestMethod1()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.InitialStatus = new Status();            
            UnitSet us = new UnitSet(CharUnits.AtoZ);
            us.Units.Add(CharUnits.atoz);
            VL.InitialStatus.Choices.Add(us);
            us.NextStatus = new Status();
            VL.EndStatus = us.NextStatus;

            string testString = "DJ";

            TinaValidator tv = new TinaValidator(VL);
            bool result = tv.Validate(testString.Select(m => (object)m).ToArray());
            TestContext.WriteLine(result.ToString());

            StringBuilder sb = new StringBuilder();                
            List<object> randomList = tv.CreateRandom();
            for(int i = 0; i < randomList.Count; i++)
                sb.Append(randomList[i]);
            TestContext.WriteLine(sb.ToString());
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

        //[TestMethod]
        public void JSONParse()
        {

        }

        [TestMethod]
        public void SimpleParse()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.InitialStatus = new Status();
            VL.EndStatus = new Status();
            StringToIntegerPart stip = new StringToIntegerPart();            
            UnitSet us1 = new UnitSet(CharUnits.Comma);
            Status s2 = new Status();
            stip.NextStatus = s2;
            s2.Choices.Add(us1);
            Status s3 = new Status();
            us1.NextStatus = s3;
            StringToIntegerPart stip2 = new StringToIntegerPart();
            s3.Choices.Add(stip2);
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
            Status s4 = new Status();
            stip2.NextStatus = s4;
            s4.Choices.Add(us2);
            Status s5 = new Status();
            us2.NextStatus = s5;
            StringToIntegerPart stip3 = new StringToIntegerPart();
            s5.Choices.Add(stip3);
            Status s6 = new Status();
            stip3.NextStatus = s6;
            UnitSet us3 = " CHA".ToUnitSet();
            s6.Choices.Add(us3);
            us3.NextStatus = VL.EndStatus;

            TinaValidator validator = new TinaValidator();
            validator.Logic = VL;

            using(FileStream fs = new FileStream(@"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File\A.txt", FileMode.Open))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    string s = sr.ReadToEnd();
                    List<object> thing = s.Select(m => (object)m).ToList();
                    validator.Validate(thing);
                }
            }
            
            
            //12, 56 70 CHA
            //08, 32 45 CHR
        }

        public void TestJsonConvert()
        {

        }
    }
}
