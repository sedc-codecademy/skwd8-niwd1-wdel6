using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ServerEntities
{
    public class HeaderCollection: IDictionary<string, string>
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        public HeaderCollection()
        {
        }

        public HeaderCollection(Dictionary<string, string> headers)
        {
            foreach (var kvp in headers)
            {
                data.Add(kvp.Key, kvp.Value);
            }
        }

        public HeaderCollection(IEnumerable<Header> headers)
        {
            foreach (var header in headers)
            {
                data.Add(header.Name, header.Value);
            }
        }

        public int Count { 
            get
            {
                return data.Count;
            }
        }

        public IEnumerable<string> Keys => ((IDictionary<string, string>)data).Keys;

        public IEnumerable<string> Values => ((IDictionary<string, string>)data).Values;

        ICollection<string> IDictionary<string, string>.Keys => ((IDictionary<string, string>)data).Keys;

        ICollection<string> IDictionary<string, string>.Values => ((IDictionary<string, string>)data).Values;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, string>>)data).IsReadOnly;

        string IDictionary<string, string>.this[string key] { get => ((IDictionary<string, string>)data)[key]; set => ((IDictionary<string, string>)data)[key] = value; }

        public string this[string key] => ((IDictionary<string, string>)data)[key];

        public bool HasHeader(string name)
        {
            return data.ContainsKey(name);
        }

        public Header GetHeader(string name)
        {
            if (!data.ContainsKey(name))
            {
                throw new ArgumentException("name");
                // return Header.Empty;
            }
            return new Header(name, data[name]);
        }

        public void SetHeader(string name, string value)
        {
            if (data.ContainsKey(name))
            {
                data[name] = value;
            }
            else
            {
                data.Add(name, value);
            }
        }

        public IEnumerable<string> GetAvailableHeaders()
        {
            return data.Keys;
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, string>)data).ContainsKey(key);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
        {
            return ((IDictionary<string, string>)data).TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)data).GetEnumerator();
        }

        public void Add(string key, string value)
        {
            ((IDictionary<string, string>)data).Add(key, value);
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, string>)data).Remove(key);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            ((ICollection<KeyValuePair<string, string>>)data).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<string, string>>)data).Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>)data).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, string>>)data).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>)data).Remove(item);
        }
    }
}
