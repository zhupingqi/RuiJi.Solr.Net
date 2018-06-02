using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Regards.Solr.Net.Handler
{
    public enum SolrUpdateRequestMethod
    { 
        Add,
        Set,
        Delete,
        Commit
    }

    /// <summary>
    /// 更新请求对象
    /// </summary>
    public class SolrUpdateRequest : ISolrRequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commitWithin { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string overwrite { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string boost { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commit { get; set; }

        [JsonIgnore]
        public SolrUpdateRequestMethod RequestMethod { get; set; }

        [JsonIgnore]
        public List<object> docs { get; private set; }

        public SolrUpdateRequest()
        {
            docs = new List<object>();
            //commitWithin = "10000";
            //overwrite = "true";
            //commit = "false";
            //boost = "1.0";
        }

        public string GetQuery()
        {
            var list = GetQuery(this);

            return string.Join("&", list.ToArray());
        }

        private List<string> GetQuery(object o)
        {
            JObject obj = JObject.FromObject(o);

            var list = new List<string>();

            foreach (var p in obj.Properties())
            {
                if (p.Value.Type == JTokenType.Null)
                    continue;

                list.Add(p.Name + "=" + HttpUtility.UrlEncode(p.Value.ToString()));
            }

            return list;
        }

        public void Add(object doc)
        {
            docs.Add(doc);
        }
    }
}