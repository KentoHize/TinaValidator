using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public const int Invalid = -1;
        public ValidateLogic Logic { get; set; }
        Calculator.Calculator CalMain { get; set; } = new Calculator.Calculator();

        public long LongerErrorLocation { get; set; }
        public TNode ErrorNode { get; set; }
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
            TVData tv = new TVData(0, Logic.InitialStatus);
            return BFS_NodeValidate(things, tv) != Invalid;
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        private int BFS_NodeValidate(List<object> things, TVData data)
        {
            Queue<TVData> nodeQueue = new Queue<TVData>();
            HashSet<TVData> invisitedRecords = new HashSet<TVData>();
            nodeQueue.Enqueue(data);
            while (nodeQueue.Count != 0)
            {
                if (data.Index >= things.Count)
                    break;
                if (data.Index > LongerErrorLocation)
                    LongerErrorLocation = data.Index;
                int nextIndex = Invalid;
                TVData newData;
                data = nodeQueue.Dequeue();                
                switch (data.Node)
                {
                    case Part p:
                        nextIndex = p.Validate(things, data.Index);
                        if (nextIndex == Invalid)
                            continue;
                        newData = new TVData(nextIndex, p.NextNode, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case Status st:
                        if (!invisitedRecords.Add(data))
                            continue;
                        for (int i = 0; i < st.Choices.Count; i++)
                        {
                            if (st.Choices[i].Conditon == null || Calculator.Calculator.CalculateBooleanExpression(st.Choices[i].Conditon, data.Memory))
                            {
                                if (i == 0)
                                    newData = new TVData(data.Index, st.Choices[i].Node, data.AreaNextNode, data.Memory);
                                else
                                {
                                    Stack<TNode> newStack = new Stack<TNode>(data.AreaNextNode.Reverse());
                                    newData = new TVData(data.Index, st.Choices[i].Node, newStack, new TVMemory(data.Memory));
                                }
                                nodeQueue.Enqueue(newData);
                            }
                        }
                        break;
                    case EndNode _:
                        if (data.AreaNextNode.Count == 0)
                            break;
                        TNode tn = data.AreaNextNode.Pop();
                        newData = new TVData(data.Index, tn, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case AreaStart ars:
                        data.AreaNextNode.Push(ars.NextNode);
                        newData = new TVData(data.Index, ars.Area.InitialStatus, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case Execute ex:
                        Calculator.Calculator.RunStatements(ex.Statements, data.Memory);
                        newData = new TVData(data.Index, ex.NextNode, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    default:
                        throw new Exception("!?");
                }
            }
            if (data.Index == things.Count)
                return data.Index;
            return Invalid;
        }

        //private int NodeValidate(List<object> things, int index, TNode node, int depth)
        //{
        //    int nextIndex = Invalid;
        //    bool isLongTesting = false;
        //    if(index > LongerErrorLocation)
        //    { 
        //        //if(index != things.Count)
        //        //{ 
        //            LongerErrorLocation = index;
        //            ErrorNode = node;
        //            isLongTesting = true;
        //        //}
        //    }
        //    switch (node)
        //    {
        //        case EndNode _:
        //            if (depth == 0)
        //                return index;
        //            else
        //            {
        //                AreaStart ars = EntryPoints[depth - 1];
        //                EntryPoints.RemoveAt(EntryPoints.Count - 1);
        //                nextIndex = NodeValidate(things, index, ars.NextNode, depth - 1);
        //                EntryPoints.Add(ars);
        //                return nextIndex;
        //            }                        
        //        case AreaStart ars:
        //            EntryPoints.Add(ars);
        //            nextIndex = NodeValidate(things, index, ars.Area.InitialStatus, depth + 1);
        //            EntryPoints.RemoveAt(EntryPoints.Count - 1);
        //            return nextIndex;
        //        case Execute e:
        //            CalMain.RunStatements(e.Statements);
        //            nextIndex = index;
        //            break;
        //        case Part p:
        //            if (index == things.Count)
        //                break;
        //            else
        //                nextIndex = p.Validate(things, index);
        //            break;
        //        case Status st:
        //            for (int i = 0; i < st.Choices.Count; i++)
        //            {
        //                if (st.Choices[i].Conditon == null || CalMain.CalculateCompareExpression(st.Choices[i].Conditon)) // TO DO (置換記憶體模式)
        //                {   
        //                    nextIndex = NodeValidate(things, index, st.Choices[i].Node, depth);
        //                    if (nextIndex != Invalid)
        //                        return nextIndex;
        //                }
        //            }
        //            return Invalid;
        //    }
        //    if (nextIndex != Invalid)
        //        return NodeValidate(things, nextIndex, node.NextNode, depth);
        //    return Invalid;
        //}

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
