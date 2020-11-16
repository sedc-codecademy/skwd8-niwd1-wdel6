using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SedcJsonSerializer
{



    public class Serializer
    {
        private static Func<string, string> stringConversion = source => $"\"{source}\"";
        private static Func<object, string> numberConversion = source => $"{source}";
        private static Func<bool, string> boolConversion = source => source.ToString().ToLowerInvariant();

        private static Dictionary<Type, Func<object, string>> typeConversions = new Dictionary<Type, Func<object, string>>
        {
            {typeof(bool), (source)=>boolConversion((bool)source)},
            {typeof(string), (source)=>stringConversion((string)source)},
            {typeof(uint), numberConversion},
            {typeof(int), numberConversion},
            {typeof(long), numberConversion},
            {typeof(ulong), numberConversion},
            {typeof(short), numberConversion},
            {typeof(ushort), numberConversion},
        };

        public static string Serialize<T>(T source)
        {
            var type = source.GetType();
            if (typeConversions.ContainsKey(type)) {
                var convertor = typeConversions[type];
                return convertor(source);
            }

            if (typeof(IEnumerable).IsAssignableFrom(type)) {
                // the source is array-ish
                var collection = source as IEnumerable;
                StringBuilder sb = new StringBuilder("[");
                var itemResults = new List<string>();
                foreach (var item in collection)
                {
                    var itemResult = Serialize(item);
                    itemResults.Add(itemResult);
                }
                sb.Append(string.Join(", ", itemResults));
                sb.Append("]");
                return sb.ToString();
            } 
            else
            {
                // the source is a composite object
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                StringBuilder sb = new StringBuilder("{");
                var itemResults = new List<string>();
                foreach (var prop in properties)
                {
                    var item = prop.GetValue(source);
                    var itemResult = Serialize(item);
                    itemResults.Add($"\"{prop.Name}\": {itemResult}");
                }
                sb.Append(string.Join(", ", itemResults));
                sb.Append("}");
                return sb.ToString();
            }

        }
    }
}
