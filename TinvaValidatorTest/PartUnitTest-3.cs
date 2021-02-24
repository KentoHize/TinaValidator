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
            List<object> result;
            AnyStringPart asp = new AnyStringPart();
            Assert.IsTrue(asp.Validate("1`6dsa8/05".ToObjectList()) == 10);
            Assert.IsTrue(asp.Validate("A-5.5".ToObjectList()) == 5);
            Assert.IsTrue(asp.Validate(new List<object> { "A-rdddv J" }) == 1);
            Assert.IsTrue(asp.Validate("136\"d56\"3".ToObjectList()) == 3);
            Assert.IsTrue(asp.Validate("-1\n75\n5535dd".ToObjectList()) == 12);
            Assert.IsTrue(asp.Validate(new List<object> { 7 }) == 0);
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp = new AnyStringPart(null, null, null, "EndFile");
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp.MaxLength = 20;
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            Assert.IsTrue(asp.Validate(asp.Random()) != -1);
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp = new AnyStringPart(null, null, null, new List<string> { "AA", "BB", "CCC" }, 10, 200);
            Assert.IsTrue(asp.Validate("rrrd145ddaabb".ToObjectList()) == 13);
            Assert.IsTrue(asp.Validate("dd64416d".ToObjectList()) == -1);
            Assert.IsTrue(asp.Validate("dd6t4416dBDDBB".ToObjectList()) == 14);
            Assert.IsTrue(asp.Validate("dd64416d465dsa534586c89das".ToObjectList()) == 26);
            result = asp.Random();
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());

            asp = new AnyStringPart();
            asp.EscapeChars.Clear();
            asp.MaxLength = 10;
            asp.MinLength = 5;
            Assert.IsTrue(asp.Validate("rrrdddaaabvbb".ToObjectList()) == -1);
            Assert.IsTrue(asp.Validate("ddd".ToObjectList()) == -1);

            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 10);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            TestContext.WriteLine(asp.Random().ForEachToString());
            asp.MaxLength = 200;
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            asp.RandomEndChance = 0.01d;
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());
            result = asp.Random();
            Assert.IsTrue(result.Count >= 5 && result.Count <= 200);
            Assert.IsTrue(asp.Validate(result) != -1);
            TestContext.WriteLine(result.Count.ToString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => asp.RandomEndChance = -0.2);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => asp.RandomEndChance = 2.3);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => asp.MinLength = -9);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => asp.MaxLength = -7);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => asp.MinLength = 255);
        }
    }
}
