using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aritiafel.Artifacts.TinaValidator;
using Aritiafel;
using System.Collections.Generic;
using System;

namespace TinvaValidatorTest
{   
    public partial class PartUnitTest
    {   

        [TestMethod]
        public void CharsToBooleanUnitTest()
        {
            CharsToBooleanPart ctbp = new CharsToBooleanPart();            
            Assert.IsTrue(ctbp.Validate("trUe".ToObjectList()) == 4);
            Assert.IsTrue(ctbp.Validate("faLSe".ToObjectList()) == 5);
            Assert.IsTrue(ctbp.Validate("faLse458".ToObjectList()) == 5);
            Assert.IsTrue(ctbp.Validate("faLde".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate(new List<object> { 3 }) == -1);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            ctbp = new CharsToBooleanPart(true);
            Assert.IsTrue(ctbp.Validate("TRUe".ToObjectList()) == 4);
            Assert.IsTrue(ctbp.Validate("FalSE".ToObjectList()) == -1);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());

            ctbp = new CharsToBooleanPart(false);
            Assert.IsTrue(ctbp.Validate("TRUe".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate("dfalSE".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate("FalSepo".ToObjectList()) == 5);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());
        }

       
    }
}
