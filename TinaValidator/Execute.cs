using Aritiafel.Artifacts.Calculator;
using System.Collections.Generic;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Execute : TNode
    {
        public bool RunRandomStatement { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Statement> RandomStatements { get; set; }

        public Execute(Statement statement, TNode nextNode = null, string id = null)
            : base(nextNode, id ?? IdentifyShop.GetNewID("E"))
        {
            Statements = new List<Statement> { statement };
        }
        public Execute(List<Statement> statements = null,
            List<Statement> ranStatements = null,
            TNode nextNode = null, string id = null)
            : base(nextNode, id ?? IdentifyShop.GetNewID("E"))
        {
            Statements = statements ?? new List<Statement>();
            RandomStatements = ranStatements;
            if (RandomStatements != null)
                RunRandomStatement = true;
        }
    }
}
