using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class AreaJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == typeof(Area) || typeToConvert.IsSubclassOf(typeof(Area));
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(AreaJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { }, null);
        private class AreaJsonConverterInner<T> : JsonConverter<T> where T : Area
        {        
            public AreaJsonConverterInner()
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
                writer.WriteStartObject();
                Type valueType = value.GetType();              
                PropertyInfo[] pis = valueType.GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (pi.GetAccessors(true)[0].IsStatic)
                        continue;
                    else if (pi.Name == "InitialStatus")
                    {
                        writer.WriteString(pi.Name, (pi.GetValue(value) as TNode).ID);
                    }
                    else if (pi.PropertyType == typeof(Area) && pi.GetValue(value) != null)
                    {
                        writer.WriteString(pi.Name, (pi.GetValue(value) as Area).Name);
                    }
                    else
                    {
                        JsonConverter jc = options.GetConverter(pi.PropertyType);
                        writer.WritePropertyName(pi.Name);
                        jc.GetType().GetMethod("Write").Invoke(jc, new object[] { writer, pi.GetValue(value), options });
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
