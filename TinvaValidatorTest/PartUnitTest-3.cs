using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TinvaValidatorTest
{
    public partial class PartUnitTest
    {

        [TestMethod]
        public void AnyStringTest()
        {
            AnyStringPart asp = new AnyStringPart();
            Assert.IsTrue(asp.Validate("1`6dsa8/05".ToObjectList()) == 10);
            Assert.IsTrue(asp.Validate("A-5.5".ToObjectList()) == 5);
            Assert.IsTrue(asp.Validate(new List<object> { "A-rdddv J" }) == 1);
            Assert.IsTrue(asp.Validate("136\"d56\"3".ToObjectList()) == 3);
            Assert.IsTrue(asp.Validate("-1\n75\n5535dd".ToObjectList()) == 12);            
            Assert.IsTrue(asp.Validate(new List<object> { 7 }) == 0);
            asp.Random();
            asp.Random();
            TestContext.WriteLine(asp.Random().ForEachToString());            
            asp = new AnyStringPart(null, null, "EndFile");
            asp.Random();
            asp.Random();
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp.MaxLength = 20;
            asp.Random();
            asp.Random();
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp = new AnyStringPart(null, null, new List<string> { "AA", "BB", "CCC" }, 10, 200);
            TestContext.WriteLine(asp.Random().ForEachToString());
            TestContext.WriteLine(asp.Random().ForEachToString());
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp = new AnyStringPart();
            asp.EscapeChars.Clear();
            asp.MaxLength = 10;
            asp.MinLength = 5;
            List<object> result;
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            TestContext.WriteLine(result.Count.ToString());
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp.MaxLength = 200;
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            TestContext.WriteLine(result.Count.ToString());
            asp.RandomEndChancePercent

            //Assert.IsTrue(asp.Validate("203587631978drd".ToObjectList()) == 12);
            //Assert.IsTrue(asp.Validate("203587631979".ToObjectList()) == -1);
            //Assert.IsTrue(asp.Validate("203587631977".ToObjectList()) == -1);
            //TestContext.WriteLine(asp.Random().ForEachToString());
            //TestContext.WriteLine(asp.Random().ForEachToString());
            //asp = new CharsToIntegerPart(-25549, 5678913);
            //Assert.IsTrue(asp.Validate("0658d".ToObjectList()) == 4);
            //Assert.IsTrue(asp.Validate("-12253".ToObjectList()) == 6);
            //Assert.IsTrue(asp.Validate("-37253".ToObjectList()) == -1);
            //TestContext.WriteLine(asp.Random().ForEachToString());
            //TestContext.WriteLine(asp.Random().ForEachToString());
            //TestContext.WriteLine(asp.Random().ForEachToString());
            //asp = new CharsToIntegerPart(6895663, -57661);
            //Assert.ThrowsException<ArgumentException>(() => asp.Random());
        }        
    }
}
