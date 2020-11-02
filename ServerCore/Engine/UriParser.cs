using ServerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace ServerCore.Engine
{
    public class UriParser
    {
        public static UriParts Parse(string uri)
        {
            bool isValidUri = Regex.IsMatch(uri, @"^(\/?\w+\/?)+\??(\w+=\w+&?)*$");
            if (!isValidUri) throw new ArgumentException("uri");

            if (!uri.Contains('?'))
            {
                var uriPaths = uri.Split('/');
                var emptyQuery = new Dictionary<string, string>();
                var fullUri = GetFullUri(uriPaths, emptyQuery);

                return new UriParts { Paths = uriPaths, Query = emptyQuery, Uri = fullUri };
            }

            var pathAndQuery = uri.Split('?');
            string path = pathAndQuery[0], query = pathAndQuery[1];
            var paths = path.Split('/');

            var queryCollection = new QueryCollection();
            queryCollection.SetQuery(query);
            var queries = queryCollection.Queries;

            return new UriParts { Paths = paths, Query = queries, Uri = GetFullUri(paths, queries) };
        }

        public static string GetFullUri(string[] paths, Dictionary<string, string> query)
        {
            string path = string.Join("/", paths);

            var keyValueQueries = query.Select(kvp => $"{kvp.Key}={kvp.Value}");
            string finalQuery = string.Join("&", keyValueQueries);

            string finalUri = (finalQuery == string.Empty) ? path : string.Join("?", path, finalQuery);

            return finalUri;
        }
    }
}
