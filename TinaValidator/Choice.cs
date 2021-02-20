using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Choice
    {   
        public TNode Node { get; set; }
        public IBoolean Conditon { get; set; }
        public int RadomRatio { get; set; }
        public static Choice EndChoice => new Choice(EndNode.Instance);
        public Choice(TNode node, IBoolean conditon = null, int radomRatio = 1)
        {
            Node = node;
            Conditon = conditon;
            RadomRatio = radomRatio;
        }
    }
}
