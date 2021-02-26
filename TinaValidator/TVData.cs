using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TVData
    {
        public int Index { get; set; }
        public TNode Node { get; set; }
        public Stack<TNode> AreaNextNode { get; set; }
        public TVData(int index, TNode node, Stack<TNode> areaNextNode = null)
        {
            Index = index;
            Node = node;
            AreaNextNode = areaNextNode ?? new Stack<TNode>();
        }
        public TVData(TVData ti)
        {
            Index = ti.Index;
            Node = ti.Node;
            AreaNextNode = ti.AreaNextNode;
        }

        public static bool operator ==(TVData a, TVData b)
            => a.Equals(b);
        public static bool operator !=(TVData a, TVData b)
            => !(a == b);

        public override int GetHashCode()
        {
            int result = Index.GetHashCode() ^ Node.ID.GetHashCode();
            if (AreaNextNode != null && AreaNextNode.Count != 0)
                result ^= AreaNextNode.Peek().GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TVData ti))
                return false;
            if (AreaNextNode.Count != ti.AreaNextNode.Count)
                return false;
            if (AreaNextNode.Peek().ID != ti.AreaNextNode.Peek().ID)
                return false;
            return ti.Index == Index && ti.Node.ID == Node.ID;
        }
    }
}
