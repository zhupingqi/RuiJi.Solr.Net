using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RuiJi.Solr.Net.Handler
{
    /// <summary>
    /// 查询请求对象
    /// </summary>
    public class SolrSelectRequest : ISolrRequest
    {
        public string q { get; set; }

        /// <summary>
        /// 在结果中查询
        /// </summary>
        public List<string> fq { get; set; }

        public string sort { get; set; }

        public int start { get; set; }

        public int rows { get; set; }

        public string fl { get; set; }

        public string df { get; set; }

        [JsonProperty("json.facet")]
        public string jsonFacet { get; set; }

        public string facet { get; set; }

        public string indent { get; set; }

        [JsonProperty("facet.pivot")]
        public string facetPivot { get; set; }

        [JsonProperty("facet.limit")]
        public string facetlimit { get; set; }

        public string collection { get; set; }

        public List<IRequestOptions> ExtraOptions
        {
            get;
            private set;
        }

        public SolrSelectRequest()
        {
            rows = 10;
            fq = new List<string>();
            ExtraOptions = new List<IRequestOptions>();
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

                if (p.Value.Type == JTokenType.Array)
                {
                    switch (p.Name)
                    {
                        case "ExtraOptions":
                            foreach (var item in p.Value)
                            {
                                foreach (var i in JObject.FromObject(item).Properties())
                                {
                                    list.Add(i.Name + "=" + HttpUtility.UrlEncode(i.Value.ToString()));
                                }
                            }
                            break;

                        default:
                            foreach (var item in p.Value)
                            {
                                list.Add(p.Name + "=" + HttpUtility.UrlEncode(item.ToString()));
                            }
                            break;
                    }

                }
                else
                    list.Add(p.Name + "=" + HttpUtility.UrlEncode(p.Value.ToString()));
            }

            return list;
        }
    }
}