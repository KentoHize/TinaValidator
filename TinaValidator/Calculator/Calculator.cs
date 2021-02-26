using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class Calculator
    {
        IMemory Memory { get; set; }
        public List<Statement> Statements { get; set; }
        public Calculator()
            : this(null, null)
        { }

        public Calculator(IMemory memory)
            : this(null, memory)
        { }

        public Calculator(List<Statement> statements = null, IMemory memory = null)
        {
            Memory = memory ?? new CalculatorMemory();
            Statements = statements ?? new List<Statement>();
        }

        public void Run()
            => RunStatements(Statements);

        public static void RunStatements(List<Statement> statements, IMemory memory)
        {
            foreach (Statement st in statements)
                RunStatement(st, memory);
        }
        public static void RunStatement(Statement statement, IMemory memory)
        {
            switch (statement)
            {
                case DeclareVariableStatement dvs:
                    memory.DeclareVariable(dvs.Name, dvs.Type, dvs.Dimension, dvs.Counts, dvs.InitialValue);
                    break;
                case SetVariableStatement svs:
                    memory.SetValue(svs.Variable, svs.Value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public void RunStatements(List<Statement> statements)
            => RunStatements(statements);
        public void RunStatement(Statement statement)
            => RunStatement(statement, Memory);
        public bool CalculateBooleanExpression(IBoolean be)
            => be.GetResult(Memory);
        public static bool CalculateBooleanExpression(IBoolean be, IMemory memory)
            => be.GetResult(memory);
        public object CalculateArithmeticExpression(INumber ae)
            => ae.GetResult(Memory).Value;
        public static object CalculateArithmeticExpression(INumber ae, IMemory memory)
            => ae.GetResult(memory).Value;
        public string CalculateStringExpression(IString se)
            => se.GetResult(Memory);
        public static string CalculateStringExpression(IString se, IMemory memory)
            => se.GetResult(memory);
        public void ClearMemory()
            => Memory.ClearVariables();
    }
}
