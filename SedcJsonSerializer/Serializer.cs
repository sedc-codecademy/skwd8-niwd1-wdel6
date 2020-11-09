using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            if (typeConversions.ContainsKey(typeof(T))) {
                var convertor = typeConversions[typeof(T)];
                return convertor(source);
            }

            return "not-working";
        }
    }
}
