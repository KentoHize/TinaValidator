using Aritiafel.Artifacts.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TinvaValidatorTest
{
    [TestClass]
    public class RunStatementTest
    {

        [TestMethod]
        public void DeclareVariable()
        {
            DeclareVariableStatement dvs = new DeclareVariableStatement("A", typeof(bool));
            Calculator cal = new Calculator();
            cal.RunStatement(dvs);
        }

        [TestMethod]
        public void SetVariable()
        {

        }

        [TestMethod]
        public void GetVariable()
        {

        }
    }
}
