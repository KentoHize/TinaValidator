using Aritiafel.Artifacts.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            StringVar sv = new StringVar("A");            
            DeclareVariableStatement dvs = new DeclareVariableStatement(sv.Name, typeof(INumber));
            SetVariableStatement svs = new SetVariableStatement(sv, new StringConst("aaa"));
            Calculator cal = new Calculator();
            Assert.ThrowsException<KeyNotFoundException>(() => cal.RunStatement(svs));

            cal.Statements.Add(dvs);
            cal.Statements.Add(svs);
            cal.Run();

            
        }

        [TestMethod]
        public void GetVariable()
        {

        }
    }
}
