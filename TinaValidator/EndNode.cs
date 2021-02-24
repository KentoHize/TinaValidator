namespace Aritiafel.Artifacts.TinaValidator
{
    public class EndNode : TNode
    {
        public static EndNode Instance { get; } = new EndNode();
        private EndNode(string id = "__ENDNODE62ADB0E6")
            : base(null, null, id)
        { }
    }
}
