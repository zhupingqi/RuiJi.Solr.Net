using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Regards.Solr.Net.Test.Document
{
    public class MLTDocument
    {
        public string id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double score { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string media { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int words { get; set; }

        [JsonConverter(typeof(SolrDateTimeConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime newstime { get; set; }
    }
}