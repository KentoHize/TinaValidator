using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public const string SaveLoadPath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\SaveLoad";
        public const string NumberFilePath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File";

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
        public void FirstTest()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            DeclareVariableStatement dvs = new DeclareVariableStatement("Times", typeof(INumber));
            SetVariableStatement svs = new SetVariableStatement(new LongVar("Times"), new LongConst(0));
            Execute initialEx = new Execute();
            initialEx.Statements.Add(dvs);
            initialEx.Statements.Add(svs);
            SetVariableStatement svs2 = new SetVariableStatement(new LongVar("Times"),
                new ArithmeticExpression(new LongVar("Times"), null, Operator.PlusOne));
            Execute ex2 = new Execute(svs2);
            CompareExpression AtLeast2 = new CompareExpression(new LongVar("Times"), new LongConst(2), Operator.GreaterThanOrEqualTo);
            Area ar1 = new Area(null, new Status(), VL);
            VL.Areas.Add(ar1);
            AreaStart ap1 = new AreaStart(ar1, null, new Status());

            VL.InitialStatus.Choices.Add(new Choice(initialEx));
            initialEx.NextNode = ap1;

            CharsToIntegerPart stip = new CharsToIntegerPart();
            ar1.InitialStatus.Choices.Add(new Choice(stip));
            UnitSet us1 = new UnitSet(CharUnits.Comma);
            us1.Units.Add(CharUnits.WhiteSpace);
            stip.NextNode = us1;
            CharsToIntegerPart stip2 = new CharsToIntegerPart();
            us1.NextNode = stip2;
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
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
            (us3.NextNode as Status).Choices.Add(new Choice(EndNode.Instance, AtLeast2));
            CRLF.NextNode = ex2;
            ex2.NextNode = ap1;
            //12, 56 70 CHA
            //08, 32 45 CHR
            //98, -3 45 CHD

            TinaValidator validator = new TinaValidator(VL);
            //
            //Run Start
            //TestContext.WriteLine(VL.Save(""));
            //return;
            bool result;            
            string[] files = Directory.GetFiles(NumberFilePath);
            for (int i = 0; i < files.Length; i++)
            {
                using (FileStream fs = new FileStream(files[i], FileMode.Open))
                {
                    using StreamReader sr = new StreamReader(fs);
                    string s = sr.ReadToEnd();
                    List<object> thing = s.ToObjectList();
                    result = validator.Validate(thing);
                }
                if (i == 0)
                    Assert.IsTrue(result);
                else
                    Assert.IsFalse(result);
            }

            for (int i = 0; i < 100; i++)
            {
                List<object> list = validator.CreateRandom();
                TestContext.WriteLine(list.ForEachToString());
                result = validator.Validate(list);
                TestContext.WriteLine("");
                Assert.IsTrue(result);
            }

            VL.Save(Path.Combine(SaveLoadPath, "Main1.json"));
            return;
        }

        [TestMethod]
        public void JsonTest()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            Area skipChars = new Area("SkipArea", new Status(), null);
            Area objectArea = new Area("ObjectArea", new Status(), null);
            Area arrayArea = new Area("ArrayArea", new Status(), null);
            Area valueArea = new Area("ValueArea", new Status(), null);
            Area propertiesArea =  new Area("PropertiesArea", new Status(), null);
            AreaStart ap1 = new AreaStart(skipChars, VL);
            
            UnitSet us = new UnitSet(CharUnits.WhiteSpace, skipChars);
            UnitSet us2 = new UnitSet(CharUnits.CarriageReturn, skipChars);
            UnitSet us3 = new UnitSet(CharUnits.LineFeed, skipChars);
            UnitSet us4 = new UnitSet(CharUnits.HorizontalTab, skipChars);
            skipChars.InitialStatus.Choices.Add(new Choice(us));
            skipChars.InitialStatus.Choices.Add(new Choice(us2));
            skipChars.InitialStatus.Choices.Add(new Choice(us3));
            skipChars.InitialStatus.Choices.Add(new Choice(us4));
            skipChars.InitialStatus.Choices.Add(Choice.EndChoice);
            Status loopStatus = new Status(skipChars);
            us.NextNode = us2.NextNode = us3.NextNode = loopStatus;
            loopStatus.Choices.Add(new Choice(skipChars.InitialStatus));
            loopStatus.Choices.Add(Choice.EndChoice);

            UnitSet leftCurlBracket = new UnitSet(CharUnits.LeftCurlyBracket);
            UnitSet rightCurlBracket = new UnitSet(CharUnits.RightCurlyBracket);
            AreaStart skSt = new AreaStart(skipChars, objectArea);
            objectArea.InitialStatus.Choices.Add(new Choice(skSt));
            skSt.NextNode = leftCurlBracket;            
            AreaStart paSt = new AreaStart(propertiesArea, objectArea);
            skSt = new AreaStart(skipChars, objectArea);
            leftCurlBracket.NextNode = skSt;
            skSt.NextNode = paSt;
            skSt = new AreaStart(skipChars, objectArea);
            paSt.NextNode = skSt;
            skSt.NextNode = rightCurlBracket;            
            skSt = new AreaStart(skipChars, objectArea);
            rightCurlBracket.NextNode = skSt;
            skSt.NextNode = EndNode.Instance;

            UnitSet leftSquareBracket = new UnitSet(CharUnits.LeftSquareBracket);
            UnitSet rightSquareBracket = new UnitSet(CharUnits.RightSquareBracket);
            skSt = new AreaStart(skipChars, arrayArea);
            arrayArea.InitialStatus.Choices.Add(new Choice(skSt));
            skSt.NextNode = leftSquareBracket;
            skSt = new AreaStart(skipChars, arrayArea);
            leftSquareBracket.NextNode = skSt;            
            AreaStart vaSt = new AreaStart(valueArea, arrayArea);
            skSt.NextNode = vaSt;
            skSt = new AreaStart(skipChars, arrayArea);
            vaSt.NextNode = skSt;            
            Status st1 = new Status(null, arrayArea);            
            skSt.NextNode = st1;
            UnitSet us5 = new UnitSet(CharUnits.Comma, arrayArea);
            st1.Choices.Add(new Choice(us5));
            st1.Choices.Add(new Choice(rightCurlBracket));            
            us5.NextNode = vaSt;
            skSt = new AreaStart(skipChars, arrayArea);
            rightCurlBracket.NextNode = skSt;
            skSt.NextNode = EndNode.Instance;

            CharsToBooleanPart cbp = new CharsToBooleanPart();
            cbp.Parent = valueArea;
            CharsToDoublePart cdp = new CharsToDoublePart();
            cdp.Parent = valueArea;
            CharsToIntegerPart cip = new CharsToIntegerPart();
            cip.Parent = valueArea;
            AnyStringPart asp = new AnyStringPart(null, valueArea, '"', 0, 0);
            UnitSet us6 = new UnitSet(CharUnits.QuotationMark, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice(cbp));
            valueArea.InitialStatus.Choices.Add(new Choice(cip));
            valueArea.InitialStatus.Choices.Add(new Choice(cdp));
            valueArea.InitialStatus.Choices.Add(new Choice(us6));
            AreaStart oaSt = new AreaStart(objectArea, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice(oaSt));





        }

        [TestMethod]
        public void SaveTest()
        {

        }
    }
}
