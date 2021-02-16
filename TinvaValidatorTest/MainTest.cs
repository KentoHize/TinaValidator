using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            //ValidateLogic th = new ValidateLogic();
            Status st1 = new Status();
            UnitSet us = new UnitSet();
            us.NextStatus = st1;
            st1.Choices.Add(us);

            string s = JsonConvert.SerializeObject(st1); // Error
            TestContext.WriteLine(s);
            //th.InitialStatus = st1;
            ////Part se = new Part();
            ////th.Choices.Add(se);
            //Status st = new Status();
            //TestContext.WriteLine(se.ToString());

            //int a = 5;
            //string s = "ssds";
            //th.Choices.Add(s.ToUnitSet("a"));
        }

        //[TestMethod]
        public void JSONParse()
        {

        }

        [TestMethod]
        public void SimpleParse()
        {
            ValidateLogic VL = new ValidateLogic();
            VL.EndStatus = new Status();
            StringToIntegerPart stip = new StringToIntegerPart();
            //VL.Choices.Add(stip);            
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
