using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public const int Invalid = -1;
        public ValidateLogic Logic { get; set; }
        Calculator.Calculator CalMain { get; set; } = new Calculator.Calculator();

        public long LongerErrorLocation { get; set; }
        public TNode ErrorNode { get; set; }

        public List<AreaStart> EntryPoints = new List<AreaStart>();

        public TinaValidator(ValidateLogic logic = null)
        {
            Logic = logic;
        }

        public string Message { get; set; }
        public bool Validate(List<object> things)
        {
            if (things == null)
                throw new ArgumentNullException(nameof(things));
            CalMain.ClearMemory();
            LongerErrorLocation = 0;
            EntryPoints = new List<AreaStart>();
            return NodeValidate(things, 0, Logic.InitialStatus, 0) != Invalid;
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        //private int NodeValidate(List<object> things, TempInfo currentTi)
        //{
        //    Queue<TempInfo> nodeQueue = new Queue<TempInfo>();
        //    nodeQueue.Enqueue(currentTi);
        //    while (nodeQueue.Count != 0)
        //    {

        //        if (currentTi.Index > LongestLocation)
        //        {
        //            LongestLocation = currentTi.Index;
        //            if (currentTi.Index == 79)
        //                Console.WriteLine("79");
        //        }

        //        int nextIndex = -1;
        //        TempInfo ti;
        //        currentTi = nodeQueue.Dequeue();
        //        switch (currentTi.Node)
        //        {
        //            case Part p:
        //                nextIndex = p.Validate(things, currentTi.Index);
        //                if (nextIndex == -1)
        //                {
        //                    currentTi.AreaNextNode.Clear();
        //                    continue;
        //                }
        //                ti = new TempInfo(nextIndex, p.NextNode, currentTi.AreaNextNode);
        //                nodeQueue.Enqueue(ti);
        //                break;
        //            case Status st:
        //                if (st.ID == "st4")
        //                {
        //                    Console.WriteLine("");
        //                }
        //                for (int i = 0; i < st.Choices.Count; i++)
        //                {
        //                    if (true) //Condition ok
        //                    {
        //                        if (i == 0)
        //                            ti = new TempInfo(currentTi.Index, st.Choices[i].Node, currentTi.AreaNextNode);
        //                        else
        //                            ti = new TempInfo(currentTi.Index, st.Choices[i].Node, new Stack<TNode>(currentTi.AreaNextNode));
        //                        nodeQueue.Enqueue(ti);
        //                    }
        //                }
        //                break;
        //            case EndNode _:
        //                if (currentTi.AreaNextNode.Count == 0)
        //                    break;
        //                TNode tn = currentTi.AreaNextNode.Pop();
        //                ti = new TempInfo(currentTi.Index, tn, currentTi.AreaNextNode);
        //                nodeQueue.Enqueue(ti);
        //                break;
        //            case AreaStart ars:
        //                TNode cloneNode = null;
        //                if (ars.Area.Name == "PropertiesArea")
        //                {
        //                    cloneNode = ars.Area.InitialStatus.Choices[0].Node.NextNode.NextNode;
        //                }
        //                else if (ars.Area.Name == "StringArea")
        //                {
        //                    Console.WriteLine("in2");
        //                }
        //                if (cloneNode != null && ars.NextNode == cloneNode)
        //                {
        //                    Console.WriteLine("in");
        //                }
        //                currentTi.AreaNextNode.Push(ars.NextNode);
        //                ti = new TempInfo(currentTi.Index, ars.Area.InitialStatus, currentTi.AreaNextNode);
        //                nodeQueue.Enqueue(ti);
        //                break;
        //            case Execute ex:
        //                ti = new TempInfo(currentTi.Index, ex.NextNode, currentTi.AreaNextNode);
        //                nodeQueue.Enqueue(ti);
        //                break;
        //            default:
        //                throw new Exception("!?");
        //        }
        //    }
        //    if (currentTi.Index == things.Count)
        //        return currentTi.Index;
        //    return -1;
        //}

        private int NodeValidate(List<object> things, int index, TNode node, int depth)
        {
            int nextIndex = Invalid;
            bool isLongTesting = false;
            if(index > LongerErrorLocation)
            { 
                //if(index != things.Count)
                //{ 
                    LongerErrorLocation = index;
                    ErrorNode = node;
                    isLongTesting = true;
                //}
            }
            switch (node)
            {
                case EndNode _:
                    if (depth == 0)
                        return index;
                    else
                    {
                        AreaStart ars = EntryPoints[depth - 1];
                        EntryPoints.RemoveAt(EntryPoints.Count - 1);
                        nextIndex = NodeValidate(things, index, ars.NextNode, depth - 1);
                        EntryPoints.Add(ars);
                        return nextIndex;
                    }                        
                case AreaStart ars:
                    EntryPoints.Add(ars);
                    nextIndex = NodeValidate(things, index, ars.Area.InitialStatus, depth + 1);
                    EntryPoints.RemoveAt(EntryPoints.Count - 1);
                    return nextIndex;
                case Execute e:
                    CalMain.RunStatements(e.Statements);
                    nextIndex = index;
                    break;
                case Part p:
                    if (index == things.Count)
                        break;
                    else
                        nextIndex = p.Validate(things, index);
                    break;
                case Status st:
                    for (int i = 0; i < st.Choices.Count; i++)
                    {
                        if (st.Choices[i].Conditon == null || CalMain.CalculateCompareExpression(st.Choices[i].Conditon)) // TO DO (置換記憶體模式)
                        {   
                            nextIndex = NodeValidate(things, index, st.Choices[i].Node, depth);
                            if (nextIndex != Invalid)
                                return nextIndex;
                        }
                    }
                    return Invalid;
            }
            if (nextIndex != Invalid)
                return NodeValidate(things, nextIndex, node.NextNode, depth);
            return Invalid;
        }

        public string CreateRandomToString()
        {
            StringBuilder sb = new StringBuilder();
            List<object> randomList = CreateRandom();
            for (int i = 0; i < randomList.Count; i++)
                sb.Append(randomList[i]);
            return sb.ToString();
        }

        public List<object> CreateRandom()
        {
            List<object> result = new List<object>();
            CalMain.ClearMemory();
            NodeCreateRandom(result, Logic.InitialStatus, null);
            return result;
        }

        private void NodeCreateRandom(List<object> result, TNode node, AreaStart ap)
        {
            switch (node)
            {
                case EndNode _:
                    return;
                case AreaStart ars:
                    NodeCreateRandom(result, ars.Area.InitialStatus, ars);
                    NodeCreateRandom(result, ars.NextNode, ap);
                    break;
                case Execute e:
                    CalMain.RunStatements(e.RunRandomStatement ? e.RandomStatements : e.Statements);
                    NodeCreateRandom(result, e.NextNode, ap);
                    break;
                case Part p:
                    result.AddRange(p.Random());
                    NodeCreateRandom(result, p.NextNode, ap);
                    break;
                case Status st:
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    SortedList<int, TNode> ratioThreshold = new SortedList<int, TNode>();
                    int RationCount = 0;
                    for (int i = 0; i < st.Choices.Count; i++)
                    {
                        if (st.Choices[i].Conditon == null || CalMain.CalculateBooleanExpression(st.Choices[i].Conditon))
                        {
                            RationCount += st.Choices[i].RadomRatio;
                            ratioThreshold.Add(RationCount, st.Choices[i].Node);
                        }
                    }
                    if (ratioThreshold.Count == 0)
                        throw new Exception($"No Route in {st.ID}");
                    int index = rnd.Next(RationCount);
                    if (ratioThreshold.ContainsKey(index))
                    {
                        NodeCreateRandom(result, ratioThreshold.Values[ratioThreshold.IndexOfKey(index) + 1], ap);
                        break;
                    }
                    else
                    {
                        ratioThreshold.Add(index, null);
                        NodeCreateRandom(result, ratioThreshold.Values[ratioThreshold.IndexOfValue(null) + 1], ap);
                        break;
                    }
            }
        }
        //private int SearchInsert(IList<int> nums, int target)
        //{
        //    int low = 0, mid, high = nums.Count - 1;
        //    while (low <= high)
        //    {
        //        mid = (low + high) / 2;
        //        if (target < nums[mid])
        //            high = mid - 1;
        //        else if (target > nums[mid])
        //            low = mid + 1;
        //        else
        //            return mid;
        //    }
        //    return low;
        //}
    }
}
