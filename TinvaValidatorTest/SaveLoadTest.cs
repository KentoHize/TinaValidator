using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using System.Text.Json;
using System.IO;

namespace TinvaValidatorTest
{
    [TestClass]
    public class SaveLoadTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void LoadTest()
        {
            ValidateLogic VL = MainTest.JsonLogic();
            VL.Save(Path.Combine(MainTest.SaveLoadPath, "JSONTest.json"));


            ValidateLogic VL2 = new ValidateLogic();
            VL2.Load(Path.Combine(MainTest.SaveLoadPath, "JSONTest.json"));

            TestContext.WriteLine("aa");
            //VL.Save(Path.Combine(MainTest.SaveLoadPath, "SecondJSONTest.json"));
        }
    }
}
