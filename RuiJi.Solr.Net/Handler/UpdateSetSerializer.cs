using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Handler
{
    public class UpdateSetSerializer : ISerializer
    {
        public string IdField { get; set; }

        public string ContentType { get; set; }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public UpdateSetSerializer(string field)
        {
            this.IdField = field;

            ContentType = "application/json";
            RootElement = "/";
        }

        public string Serialize(object value)
        {
            var valueList = value as List<object>;
            var docList = new List<object>();

            foreach (var v in valueList)
            {
                JObject jObj = JObject.FromObject(v);
                var obj = new object();
                var objPropertys = JObject.FromObject(obj);

                var sp = IdField.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in sp)
                {
                    var t = jObj.GetValue(s);
                    if(t == null)
                        continue;
                    var sv= t.ToString();
                    jObj.Remove(s);
                    objPropertys.Add(s, sv);
                }

                var ps = jObj.Properties();
                foreach (var p in ps)
                {
                    var o = new
                    {
                        set = p.Value.ToObject<object>()
                    };

                    var vv = JToken.FromObject(o);
                    objPropertys.Add(p.Name, vv);
                }

                docList.Add(objPropertys);
            }

            return JsonConvert.SerializeObject(docList);
        }
    }
}