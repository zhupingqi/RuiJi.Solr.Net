using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net
{
    public class SolrResponseHeader : ISolrResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Time this operation took in ms
        /// </summary>
        [JsonProperty("QTime")]
        public int QTime { get; set; }

        /// <summary>
        /// Parameters defined in this operation
        /// </summary>
        [JsonProperty("params")]
        public IDictionary<string, string> Params { get; set; }
    }
}