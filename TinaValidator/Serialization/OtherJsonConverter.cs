using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Locations.StorageHouse;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{

    public class OtherJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(IObject).IsAssignableFrom(typeToConvert) || typeof(Unit).IsAssignableFrom(typeToConvert);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type[] type = new Type[] { null };
            type[0] = typeof(IObject).IsAssignableFrom(typeToConvert) ? typeof(IObject) : typeof(Unit);
            return (JsonConverter)Activator.CreateInstance(
                typeof(OtherJsonConverterInner<>).MakeGenericType(type),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { }, null);
        }

        private class OtherJsonConverterInner<T> : DefaultJsonConverter<T>
        {
            public OtherJsonConverterInner()
                :base()
            { }
        }
    }
}
