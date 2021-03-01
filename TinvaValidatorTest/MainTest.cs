using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            ValidateLogic VL = new ValidateLogic();
            UnitSet us = new UnitSet(CharUnits.AtoZ);
            us.Units.Add(CharUnits.atoz);
            VL.StartNode = us;
            us.NextNode = EndNode.Instance;

            string testString = "DJ";

            TinaValidator tv = new TinaValidator(VL);
            bool result = tv.Validate(testString.ToObjectList());
            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(tv.CreateRandomToString());
        }

        public static ValidateLogic FirstTestLogic()
        {
            ValidateLogic VL = new ValidateLogic();
            DeclareVariableStatement dvs = new DeclareVariableStatement("Times", typeof(INumber));
            SetVariableStatement svs = new SetVariableStatement(new LongVar("Times"), new LongConst(0));
            Execute initialEx = new Execute();
            initialEx.Statements.Add(dvs);
            initialEx.Statements.Add(svs);
            SetVariableStatement svs2 = new SetVariableStatement(new LongVar("Times"),
                new ArithmeticExpression(new LongVar("Times"), null, Operator.PlusOne));
            Execute ex2 = new Execute(svs2);
            CompareExpression AtLeast2 = new CompareExpression(new LongVar("Times"), new LongConst(2), Operator.GreaterThanOrEqualTo);
            Area ar1 = new Area(null, null, VL);
            VL.Areas.Add(ar1);
            AreaStart ap1 = new AreaStart(ar1);

            VL.StartNode = initialEx;
            initialEx.NextNode = ap1;

            CharsToIntegerPart stip = new CharsToIntegerPart();
            ar1.StartNode = stip;
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
            Status st = new Status();            
            ap1.NextNode = us3;
            UnitSet CRLF = "\r\n".ToUnitSet();
            st.Choices.Add(new Choice(CRLF));
            st.Choices.Add(new Choice(EndNode.Instance, AtLeast2));
            us3.NextNode = st;
            CRLF.NextNode = ex2;
            ex2.NextNode = ap1;
            //12, 56 70 CHA
            //08, 32 45 CHR
            //98, -3 45 CHD
            return VL;
        }

        [TestMethod]
        public void FirstTest()
        {
            TinaValidator validator = new TinaValidator(FirstTestLogic());
            //--------------------------------------------------
            //Run Start            
            bool result;
            string[] files = Directory.GetFiles(NumberFilePath);
            for (int i = 0; i < files.Length; i++)
            {
                using (FileStream fs = new FileStream(files[i], FileMode.Open))
                {
                    using StreamReader sr = new StreamReader(fs);
                    string s = sr.ReadToEnd();
                    List<ObjectConst> thing = s.ToObjectList();
                    result = validator.Validate(thing);
                }
                if (i == 0)
                    Assert.IsTrue(result);
                else
                    Assert.IsFalse(result);
            }

            for (int i = 0; i < 100; i++)
            {
                List<ObjectConst> list = validator.CreateRandom();
                TestContext.WriteLine(list.ForEachToString());                
                result = validator.Validate(list);                
                TestContext.WriteLine("");
                Assert.IsTrue(result);
            }

            validator.Logic.Save(Path.Combine(SaveLoadPath, "Main1.json"));
            return;
        }

        public static ValidateLogic JsonLogic()
        {
            ValidateLogic VL = new ValidateLogic("Main");
            Area skipChars = new Area("SkipArea");
            Area objectArea = new Area("ObjectArea");
            Area arrayArea = new Area("ArrayArea");
            Area valueArea = new Area("ValueArea");
            Area propertiesArea = new Area("PropertiesArea");
            Area stringArea = new Area("StringArea");
            VL.Areas.Add(skipChars);
            VL.Areas.Add(objectArea);
            VL.Areas.Add(arrayArea);
            VL.Areas.Add(valueArea);
            VL.Areas.Add(propertiesArea);
            VL.Areas.Add(stringArea);

            //StringArea
            UnitSet us12 = new UnitSet(CharUnits.QuotationMark);
            stringArea.StartNode = us12;
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
            Status st1 = new Status();            
            st1.Choices.Add(new Choice(us));
            st1.Choices.Add(new Choice(us2));
            st1.Choices.Add(new Choice(us3));
            st1.Choices.Add(new Choice(us4));
            st1.Choices.Add(Choice.EndChoice);
            skipChars.StartNode = st1;
            us.NextNode = us2.NextNode = us3.NextNode = us4.NextNode = skipChars.StartNode;

            //Object Area
            UnitSet leftCurlBracket = new UnitSet(CharUnits.LeftCurlyBracket);
            UnitSet rightCurlBracket = new UnitSet(CharUnits.RightCurlyBracket);
            AreaStart skSt = new AreaStart(skipChars, objectArea);            
            objectArea.StartNode = leftCurlBracket;
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
            arrayArea.StartNode = leftSquareBracket;
            skSt = new AreaStart(skipChars, arrayArea);
            leftSquareBracket.NextNode = skSt;
            AreaStart vaSt = new AreaStart(valueArea, arrayArea);
            skSt.NextNode = vaSt;
            skSt = new AreaStart(skipChars, arrayArea);
            vaSt.NextNode = skSt;
            Status st2 = new Status("array_st2", arrayArea);
            skSt.NextNode = st2;
            UnitSet us5 = new UnitSet(CharUnits.Comma, arrayArea);
            st2.Choices.Add(new Choice(us5));
            st2.Choices.Add(new Choice(rightSquareBracket));
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
            Status st3 = new Status();            
            st3.Choices.Add(new Choice("null".ToUnitSet()));
            st3.Choices.Add(new Choice(cbp));
            st3.Choices.Add(new Choice(cip));
            st3.Choices.Add(new Choice(cdp));
            AreaStart oaSt = new AreaStart(objectArea, valueArea);
            st3.Choices.Add(new Choice(oaSt));
            AreaStart arSt = new AreaStart(arrayArea, valueArea);
            st3.Choices.Add(new Choice(arSt));
            st3.Choices.Add(new Choice(stSt));
            for (int i = 0; i < st3.Choices.Count; i++)
                st3.Choices[i].Node.NextNode = EndNode.Instance;
            valueArea.StartNode = st3;

            //Properties Area            
            stSt = new AreaStart(stringArea, propertiesArea);
            propertiesArea.StartNode = stSt;
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
            st6.Choices.Add(new Choice(skSt, null, 4));
            UnitSet us9 = new UnitSet(CharUnits.Comma, propertiesArea);
            skSt.NextNode = us9;
            skSt = new AreaStart(skipChars, propertiesArea);
            us9.NextNode = skSt;
            skSt.NextNode = propertiesArea.StartNode;

            //Start Main
            skSt = new AreaStart(skipChars, VL, null, "Main_SKIP1");
            VL.StartNode = skSt;
            Status JsonStartStatus = new Status("Main_ST_Object_Or_Array", VL);
            skSt.NextNode = JsonStartStatus;
            AreaStart ap1 = new AreaStart(objectArea, VL, null, "Main_AS_ObjectArea");
            AreaStart ap2 = new AreaStart(arrayArea, VL, null, "Main_AR_ArrayArea");
            skSt = new AreaStart(skipChars, VL, null, "Main_SKIP2");
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
            string[] files = Directory.GetFiles(RandomJsonPath);
            foreach (string file in files)
                File.Delete(file);
            for (int i = 0; i < 1000; i++)
            {
                List<ObjectConst> ol;
                ol = validator.CreateRandom();
                string s = ol.ForEachToString();
                byte[] buffer = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.UTF8, System.Text.Encoding.Unicode.GetBytes(s));
                s = System.Text.Encoding.UTF8.GetString(buffer);
                if (ol.Count > 10000)
                {
                    using (FileStream fs = new FileStream(Path.Combine(RandomJsonPath, $"BigJson-{i.ToString("0000")}.json"), FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(buffer);
                        bw.Close();
                    }
                    TestContext.WriteLine($"{i} ObjectCount: {ol.Count}");
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
                    //TestContext.WriteLine("Error Node:" + validator.ErrorNode.ID);
                    //TestContext.WriteLine("Node Type:" + validator.ErrorNode.GetType().Name);
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
                    JsonSerializerOptions jso = new JsonSerializerOptions 
                    { WriteIndented = true, MaxDepth = 128 };
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
            string fullName = "BigJson-0255";

            string recordNeedRun = "02-25-00-13-30-0029";
            //fullName = $"TestRecord-{DateTime.Now.Year}-{recordNeedRun}";
            List<char> ol = null;
            List<ObjectConst> oll = new List<ObjectConst>();
            using (FileStream fs = new FileStream(Path.Combine(RandomJsonPath, $"{fullName}.json"), FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                JsonSerializerOptions jso = new JsonSerializerOptions { WriteIndented = true, MaxDepth = 128 };
                jso.Converters.Add(new Aritiafel.Locations.StorageHouse.DefaultJsonConverterFactory());                
                //ol = (List<char>)JsonSerializer.Deserialize(sr.ReadToEnd(), typeof(List<char>));
                string s = sr.ReadToEnd();
                JsonSerializer.Deserialize(s, typeof(object), jso);
                oll = s.ToObjectList();
                sr.Close();
            }

            
            if(ol != null)
                for (int i = 0; i < ol.Count; i++)
                    oll.Add(new CharConst(ol[i]));

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
    }
}
