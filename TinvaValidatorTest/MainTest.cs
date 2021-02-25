using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public const string SaveLoadPath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\SaveLoad";
        public const string NumberFilePath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File";
        public const string WrongRecordsPath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\WrongRecords";
        public const string RandomJsonPath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\RandomJson";
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

        public ValidateLogic JsonLogic()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            Area skipChars = new Area("SkipArea", new Status("SkipArea_Start"), null);
            Area objectArea = new Area("ObjectArea", new Status("ObjectArea_Start"), null);
            Area arrayArea = new Area("ArrayArea", new Status("ArrayArea_Start"), null);
            Area valueArea = new Area("ValueArea", new Status("ValueArea_Start"), null);
            Area propertiesArea = new Area("PropertiesArea", new Status("PropertiesArea_Start"), null);
            Area stringArea = new Area("StringArea", new Status("StringArea_Start"), null);
            VL.Areas.Add(skipChars);
            VL.Areas.Add(objectArea);
            VL.Areas.Add(arrayArea);
            VL.Areas.Add(valueArea);
            VL.Areas.Add(propertiesArea);
            VL.Areas.Add(stringArea);

            //StringArea
            UnitSet us12 = new UnitSet(CharUnits.QuotationMark);
            stringArea.InitialStatus.Choices.Add(new Choice(us12));
            List<char> excludeChars = new List<char>
            { '\\', '\"' };
            for (int i = 0; i < 32; i++)
                excludeChars.Add((char)i);
            AnyStringPart asp1 = new AnyStringPart(null, stringArea, "asp1", new List<char> { '\\', '\"' }, excludeChars, 0, 0);
            us12.NextNode = asp1;
            Status st4 = new Status("st4");
            asp1.NextNode = st4;
            st4.Choices.Add(new Choice(new UnitSet(CharUnits.BackSlash)));
            st4.Choices.Add(new Choice(new UnitSet(CharUnits.QuotationMark)));
            st4.Choices[1].Node.NextNode = EndNode.Instance;
            Status st5 = new Status("st5");
            st4.Choices[0].Node.NextNode = st5;
            st5.Choices.Add(new Choice(new UnitSet(new CharUnit('b'))));
            st5.Choices.Add(new Choice(new UnitSet(new CharUnit('f'))));
            st5.Choices.Add(new Choice(new UnitSet(new CharUnit('n'))));
            st5.Choices.Add(new Choice(new UnitSet(new CharUnit('r'))));
            st5.Choices.Add(new Choice(new UnitSet(new CharUnit('t'))));
            st5.Choices.Add(new Choice(new UnitSet(CharUnits.Slash)));
            st5.Choices.Add(new Choice(new UnitSet(CharUnits.BackSlash)));
            st5.Choices.Add(new Choice(new UnitSet(CharUnits.QuotationMark)));
            UnitSet unicodeSet = new UnitSet(new CharUnit('u'));
            unicodeSet.Units.Add(CharUnits.HexdecimalDigit);
            unicodeSet.Units.Add(CharUnits.HexdecimalDigit);
            unicodeSet.Units.Add(CharUnits.HexdecimalDigit);
            unicodeSet.Units.Add(CharUnits.HexdecimalDigit);
            st5.Choices.Add(new Choice(unicodeSet));
            for (int i = 0; i < st5.Choices.Count; i++)
                st5.Choices[i].Node.NextNode = asp1;

            //Skip Area
            UnitSet us = new UnitSet(CharUnits.WhiteSpace, skipChars);
            UnitSet us2 = new UnitSet(CharUnits.CarriageReturn, skipChars);
            UnitSet us3 = new UnitSet(CharUnits.LineFeed, skipChars);
            UnitSet us4 = new UnitSet(CharUnits.HorizontalTab, skipChars);
            skipChars.InitialStatus.Choices.Add(new Choice(us));
            skipChars.InitialStatus.Choices.Add(new Choice(us2));
            skipChars.InitialStatus.Choices.Add(new Choice(us3));
            skipChars.InitialStatus.Choices.Add(new Choice(us4));
            skipChars.InitialStatus.Choices.Add(Choice.EndChoice);
            us.NextNode = us2.NextNode = us3.NextNode = us4.NextNode = skipChars.InitialStatus;

            //Object Area
            UnitSet leftCurlBracket = new UnitSet(CharUnits.LeftCurlyBracket);
            UnitSet rightCurlBracket = new UnitSet(CharUnits.RightCurlyBracket);
            AreaStart skSt = new AreaStart(skipChars, objectArea);
            objectArea.InitialStatus.Choices.Add(new Choice(leftCurlBracket));
            AreaStart paSt = new AreaStart(propertiesArea, objectArea);
            leftCurlBracket.NextNode = skSt;
            skSt.NextNode = paSt;
            skSt = new AreaStart(skipChars, objectArea);
            paSt.NextNode = skSt;
            skSt.NextNode = rightCurlBracket;
            rightCurlBracket.NextNode = EndNode.Instance;

            //ArrayArea
            UnitSet leftSquareBracket = new UnitSet(CharUnits.LeftSquareBracket);
            UnitSet rightSquareBracket = new UnitSet(CharUnits.RightSquareBracket);
            arrayArea.InitialStatus.Choices.Add(new Choice(leftSquareBracket));
            skSt = new AreaStart(skipChars, arrayArea);
            leftSquareBracket.NextNode = skSt;
            AreaStart vaSt = new AreaStart(valueArea, arrayArea);
            skSt.NextNode = vaSt;
            skSt = new AreaStart(skipChars, arrayArea);
            vaSt.NextNode = skSt;
            Status st1 = new Status("array_st1", arrayArea);
            skSt.NextNode = st1;
            UnitSet us5 = new UnitSet(CharUnits.Comma, arrayArea);
            st1.Choices.Add(new Choice(us5));
            st1.Choices.Add(new Choice(rightSquareBracket));
            skSt = new AreaStart(skipChars, arrayArea);
            us5.NextNode = skSt;
            skSt.NextNode = vaSt;
            rightSquareBracket.NextNode = EndNode.Instance;

            //Value Area
            CharsToBooleanPart cbp = new CharsToBooleanPart();
            cbp.Parent = valueArea;
            CharsToDoublePart cdp = new CharsToDoublePart();
            cdp.Parent = valueArea;
            CharsToIntegerPart cip = new CharsToIntegerPart();
            cip.Parent = valueArea;
            AreaStart stSt = new AreaStart(stringArea, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice("null".ToUnitSet()));
            valueArea.InitialStatus.Choices.Add(new Choice(cbp));
            valueArea.InitialStatus.Choices.Add(new Choice(cip));
            valueArea.InitialStatus.Choices.Add(new Choice(cdp));            
            AreaStart oaSt = new AreaStart(objectArea, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice(oaSt));
            AreaStart arSt = new AreaStart(arrayArea, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice(arSt));
            valueArea.InitialStatus.Choices.Add(new Choice(stSt));
            for (int i = 0; i < valueArea.InitialStatus.Choices.Count; i++)
                valueArea.InitialStatus.Choices[i].Node.NextNode = EndNode.Instance;

            //Properties Area            
            stSt = new AreaStart(stringArea, propertiesArea);
            propertiesArea.InitialStatus.Choices.Add(new Choice(stSt));
            skSt = new AreaStart(skipChars, propertiesArea);
            stSt.NextNode = skSt;
            UnitSet us8 = new UnitSet(CharUnits.Colon, propertiesArea);
            skSt.NextNode = us8;
            skSt = new AreaStart(skipChars, propertiesArea);
            us8.NextNode = skSt;
            vaSt = new AreaStart(valueArea, propertiesArea);
            skSt.NextNode = vaSt;
            Status st6 = new Status("pa_st6");
            vaSt.NextNode = st6;
            st6.Choices.Add(Choice.EndChoice);
            skSt = new AreaStart(skipChars, propertiesArea);
            st6.Choices.Add(new Choice(skSt));
            UnitSet us9 = new UnitSet(CharUnits.Comma, propertiesArea);
            skSt.NextNode = us9;
            skSt = new AreaStart(skipChars, propertiesArea);
            us9.NextNode = skSt;
            skSt.NextNode = propertiesArea.InitialStatus;

            //Start Main
            skSt = new AreaStart(skipChars, VL);
            VL.InitialStatus.Choices.Add(new Choice(skSt));
            Status JsonStartStatus = new Status(null, VL);
            skSt.NextNode = JsonStartStatus;
            AreaStart ap1 = new AreaStart(objectArea, null);
            AreaStart ap2 = new AreaStart(arrayArea, null);
            skSt = new AreaStart(skipChars, VL);
            ap1.NextNode = ap2.NextNode = skSt;
            skSt.NextNode = EndNode.Instance;
            JsonStartStatus.Choices.Add(new Choice(ap1));
            JsonStartStatus.Choices.Add(new Choice(ap2));
            return VL;
        }

        [TestMethod]
        public void JsonTest()
        {
            ValidateLogic VL = JsonLogic();

            TinaValidator validator = new TinaValidator(VL);
            VL.Save(Path.Combine(SaveLoadPath, "JSONTest.json"));
            for (int i = 0; i < 1000; i++)
            {
                List<object> ol = validator.CreateRandom();
                string s = ol.ForEachToString();
                byte[] buffer = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.UTF8, System.Text.Encoding.Unicode.GetBytes(s));
                s = System.Text.Encoding.UTF8.GetString(buffer);
                if (i < 10)
                {
                    using (FileStream fs = new FileStream(Path.Combine(RandomJsonPath, $"RandomJson-{i.ToString("0000")}.json"), FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(buffer);
                        bw.Close();
                    }
                }
                try
                {                
                    JsonSerializer.Deserialize(s, typeof(object));
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Wrong Happen:" + i);
                    TestContext.WriteLine("Message:" + ex.Message);
                    TestContext.WriteLine(s);
                    using (FileStream fs = new FileStream(Path.Combine(WrongRecordsPath, $"DeserializeRecord-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}-{i.ToString("0000")}.json"), FileMode.Create))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine($"Message: {ex.Message}");                        
                        sw.WriteLine("Content:");
                        sw.Write(s);
                        sw.Close();
                    }
                }

                if (!validator.Validate(ol))
                {
                    TestContext.WriteLine("-----------------------------");
                    TestContext.WriteLine("Wrong happen: " + i);
                    TestContext.WriteLine(ol.ForEachToString());
                    TestContext.WriteLine("TotalObjectCount:" + ol.Count);
                    TestContext.WriteLine("Error Node:" + validator.ErrorNode.ID);
                    TestContext.WriteLine("Node Type:" + validator.ErrorNode.GetType().Name);
                    string ss = "";
                    for (int j = (int)validator.LongerErrorLocation - 5; j < validator.LongerErrorLocation + 5; j++)
                    {
                        if (j >= 0 && j < ol.Count)
                            ss += ol[j].ToString();
                        if (j == validator.LongerErrorLocation)
                            ss += "!";
                    }
                    TestContext.WriteLine("NearbyString:" + ss);
                    TestContext.WriteLine("Error Location:" + validator.LongerErrorLocation);
                    JsonSerializerOptions jso = new JsonSerializerOptions { WriteIndented = true };
                    jso.Converters.Add(new Aritiafel.Locations.StorageHouse.DefaultJsonConverterFactory());
                    using (FileStream fs = new FileStream(Path.Combine(WrongRecordsPath, $"TestRecord-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}-{i.ToString("0000")}.json"), FileMode.Create))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(JsonSerializer.Serialize(ol, jso));
                        sw.Close();
                    }
                }
            }
            TestContext.WriteLine("Test End");
        }

        [TestMethod]
        public void RerunRecords()
        {
            ValidateLogic VL = JsonLogic();
            string recordNeedRun = "02-25-00-13-30-0029";

            List<char> ol;
            using (FileStream fs = new FileStream(Path.Combine(WrongRecordsPath, $"TestRecord-{DateTime.Now.Year}-{recordNeedRun}.json"), FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                JsonSerializerOptions jso = new JsonSerializerOptions { WriteIndented = true };
                jso.Converters.Add(new Aritiafel.Locations.StorageHouse.DefaultJsonConverterFactory());
                ol = (List<char>)JsonSerializer.Deserialize(sr.ReadToEnd(), typeof(List<char>));
                sr.Close();
            }

            List<object> oll = new List<object>();
            for (int i = 0; i < ol.Count; i++)
                oll.Add(ol[i]);

            TinaValidator validator = new TinaValidator(VL);
            TestContext.WriteLine(validator.Validate(oll).ToString());
        }


        [TestMethod]
        public void ClearRecords()
        {
            string[] files = Directory.GetFiles(WrongRecordsPath);
            foreach (string file in files)
                File.Delete(file);
        }

        [TestMethod]
        public void SaveTest()
        {

        }
    }
}
