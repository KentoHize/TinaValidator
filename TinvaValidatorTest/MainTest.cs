using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestParse()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            UnitSet us = new UnitSet(CharUnits.AtoZ);
            us.Units.Add(CharUnits.atoz);
            VL.InitialStatus.Choices.Add(new Choice(us));
            us.NextNode = EndNode.Instance;

            string testString = "DJ";

            TinaValidator tv = new TinaValidator(VL);
            bool result = tv.Validate(testString.Select(m => (object)m).ToArray());
            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(tv.CreateRandomToString());
        }

        [TestMethod]
        public void AreaParse()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            //Execute ex = new Execute();
            Area ar1 = new Area(null, new Status(), VL);
            AreaStart ap1 = new AreaStart(ar1, new Status());

            VL.InitialStatus.Choices.Add(new Choice(ap1));
            CharsToIntegerPart stip = new CharsToIntegerPart();
            ar1.InitialStatus.Choices.Add(new Choice(stip));
            UnitSet us1 = new UnitSet(CharUnits.Comma);            
            us1.Units.Add(CharUnits.WhiteSpace);
            stip.NextNode = us1;
            CharsToIntegerPart stip2 = new CharsToIntegerPart();
            us1.NextNode = stip2;
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
            Status s4 = new Status();
            stip2.NextNode = us2;
            CharsToIntegerPart stip3 = new CharsToIntegerPart();
            us2.NextNode = stip3;
            stip3.NextNode = EndNode.Instance;

            UnitSet us3 = " CH".ToUnitSet();
            us3.Units.Add(CharUnits.AtoZ);
            (ap1.NextNode as Status).Choices.Add(new Choice(us3));

            us3.NextNode = new Status();            
            UnitSet CRLF = "\r\n".ToUnitSet();
            (us3.NextNode as Status).Choices.Add(new Choice(CRLF));
            (us3.NextNode as Status).Choices.Add(Choice.EndChoice);
            CRLF.NextNode = VL.InitialStatus;
            //12, 56 70 CHA
            //08, 32 45 CHR

            TinaValidator validator = new TinaValidator(VL);
            bool result;
            using (FileStream fs = new FileStream(@"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File\A.txt", FileMode.Open))
            {
                using StreamReader sr = new StreamReader(fs);
                string s = sr.ReadToEnd();
                List<object> thing = s.Select(m => (object)m).ToList();
                result = validator.Validate(thing);
            }
            TestContext.WriteLine(result.ToString());

            for (int i = 0; i < 100; i++)
            {
                List<object> list = validator.CreateRandom();
                TestContext.WriteLine(list.ForEachToString());
                TestContext.WriteLine(validator.Validate(list).ToString());
            }
        }

        [TestMethod]
        public void AreaParse2()
        {

        }
    }
}
