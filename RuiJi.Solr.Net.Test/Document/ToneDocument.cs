using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Test.Document
{
    public class ToneDocument
    {
        public string id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string content { get; set; }

        //[JsonConverter(typeof(ToneSetConverter))]
        public string tone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? words { get; set; }
    }
}