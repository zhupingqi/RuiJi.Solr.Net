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
    /// 分词统计请求对象（此Solr接口需要单独扩展编写）
    /// </summary>
    public class SolrParticipleRequest : ISolrRequest
    {

        public string q { get; set; }

        public string fl { get; set; }


        [JsonProperty("qtv.fl")]
        public string qtvFl { get; set; }

        public string qtv { get; set; }

        public string indent { get; set; }

        public string collections { get; set; }

        public List<IRequestOptions> ExtraOptions
        {
            get;
            private set;
        }

        public SolrParticipleRequest()
        {
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
