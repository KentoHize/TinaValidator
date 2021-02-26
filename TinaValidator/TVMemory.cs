using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TVMemory : IMemory
    {
        Dictionary<string, ICloneable> Variables { get; set; } = new Dictionary<string, ICloneable>();
        Dictionary<string, Type> VariablesType { get; set; } = new Dictionary<string, Type>();
        private ICloneable[] CreateArray(byte dimensions, int[] counts, byte dimensionIndex = 0)
        {
            ICloneable[] result = new ICloneable[counts[dimensionIndex]];
            if (dimensions <= 0)
                return result;
            for (int i = 0; i < counts[dimensionIndex]; i++)
                result[0] = CreateArray(--dimensions, counts, ++dimensionIndex);
            return result;
        }
        public object GetValue(Variable v)
        {
            if (!Variables.ContainsKey(v.Name))
                throw new KeyNotFoundException();
            if (v.Keys == null)
                return Variables[v.Name];
            else
            {
                ICloneable[] o = Variables[v.Name] as ICloneable[];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = o[(int)v.Keys[i]] as ICloneable[];
                return o[(int)v.Keys[v.Keys.Count - 1]];
            }
        }

        public void SetValue(Variable v, IObject value)
        {
            if (!Variables.ContainsKey(v.Name))
                throw new KeyNotFoundException();
            if (v.Keys == null)
                Variables[v.Name] = value.GetObject(this);
            else
            {
                ICloneable[] o = Variables[v.Name] as ICloneable[];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = o[(int)v.Keys[i]] as ICloneable[];
                o[(int)v.Keys[v.Keys.Count - 1]] = value;
            }
        }

        public bool DeclareVariable(string name, Type type, byte dimensions = 0, int[] counts = null, IObject initialValue = null)
        {
            if (dimensions > 10 || dimensions < 0)
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            if (Variables.ContainsKey(name))
                return false;

            if (dimensions == 0)
            {
                Variables.Add(name, initialValue?.GetObject(this));
                VariablesType.Add(name, type);
            }
            else
            {
                ICloneable[] array = CreateArray(dimensions, counts);
                Variables.Add(name, array);
                VariablesType.Add(name, type);
            }
            return true;
        }

        public bool DeleteVariable(string name)
        {
            if (!Variables.ContainsKey(name))
                return false;
            Variables.Remove(name);
            VariablesType.Remove(name);
            return true;
        }

        public void ClearVariables()
        {
            Variables.Clear();
            VariablesType.Clear();
        }

        public TVMemory()
        { }

        public TVMemory(TVMemory memory)
        {
            memory.Variables = new Dictionary<string, ICloneable>();
            foreach(KeyValuePair<string, ICloneable> kv in Variables)
                memory.Variables.Add(kv.Key, kv.Value.Clone() as ICloneable);
            memory.VariablesType = new Dictionary<string, Type>(VariablesType);
        }
    }
}
