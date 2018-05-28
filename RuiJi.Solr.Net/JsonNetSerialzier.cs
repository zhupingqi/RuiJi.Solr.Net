using Newtonsoft.Json;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net
{
    public class JsonNetSerialzier : ISerializer
    {
        public string ContentType { get; set; }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public JsonNetSerialzier()
        {
            ContentType = "application/json";
            RootElement = "/";
        }

        public string Serialize(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return json;
        }
    }
}
