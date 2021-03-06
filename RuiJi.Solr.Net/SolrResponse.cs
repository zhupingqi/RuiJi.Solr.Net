﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RuiJi.Solr.Net
{
    public class SolrResponse : ISolrResponse
    {
        [JsonProperty("responseHeader")]
        public SolrResponseHeader ResponseHeader { get; set; }

        private string content;

        static Dictionary<string, Type> _responseTypeMap;

        static SolrResponse()
        {
            _responseTypeMap = new Dictionary<string, Type>();
            _responseTypeMap.Add("responseHeader", typeof(SolrResponseHeader));
        }

        public static void AddResponseTypeMap<T>(string key)
        {
            _responseTypeMap.Add(key, typeof(T));
        }

        public static void ClearResponseTypeMaps()
        {
            _responseTypeMap.Clear();
            _responseTypeMap.Add("responseHeader", typeof(SolrResponseHeader));
        }

        public static bool RemoveResponseTypeMap(string key)
        {
            return _responseTypeMap.Remove(key);
        }

        public SolrResponse()
        { 
            
        }

        public SolrResponse(string content)
        {
            this.content = content;
        }

        public T GetData<T>()
        {
            JObject obj = JObject.Parse(content);

            var type = _responseTypeMap.SingleOrDefault(m=>m.Value == typeof(T));

            return GetData<T>(type.Key);
        }

        public T GetData<T>(string property)
        {
            if (string.IsNullOrEmpty(content))
                return default(T);
            JObject obj = JObject.Parse(content);
            JToken token = obj.SelectToken(property);

            if (token != null)
            {
                return JsonConvert.DeserializeObject<T>(token.ToString());
            }

            return default(T);
        }
    }
}