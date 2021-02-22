using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class TNodeJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == typeof(TNode) || typeToConvert.IsSubclassOf(typeof(TNode));
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(TNodeJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { }, null);
        private class TNodeJsonConverterInner<T> : JsonConverter<T> where T : TNode
        {        
            public TNodeJsonConverterInner()
            { }

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                    return;
                }
                    
                Type valueType = value.GetType();                
                PropertyInfo[] pis = valueType.GetProperties();
                writer.WriteStartObject();
                writer.WriteString("Type", valueType.Name);
                foreach (PropertyInfo pi in pis)
                {
                    if (pi.GetAccessors(true)[0].IsStatic)
                        continue;
                    
                    object p_value = pi.GetValue(value);
                    
                    if (p_value == null)
                    {
                        writer.WriteNull(pi.Name);
                        continue;
                    }
                    else if (pi.Name == "ID")
                        continue;
                    else if (pi.PropertyType == typeof(TNode))
                        writer.WriteString(pi.Name, (p_value as TNode).ID);
                    else if (pi.PropertyType == typeof(Area))
                        writer.WriteString(pi.Name, (p_value as Area).Name);
                    else
                    {   
                        JsonConverter jc = options.GetConverter(p_value.GetType());
                        writer.WritePropertyName(pi.Name);
                        jc.GetType().GetMethod("Write").Invoke(jc, new object[] { writer, p_value, options });
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
