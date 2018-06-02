using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RuiJi.Solr.Net.Handler
{
    public class MltOptions : RequestOptionsBase
    {
        [JsonProperty("mlt.interestingTerms")]
        public string interestingTerms { get; set; }

        [JsonProperty("mlt.mindf")]
        public int mindf { get; set; }

        [JsonProperty("mlt.minwl")]
        public int minwl { get; set; }

        [JsonProperty("mlt.maxqt")]
        public int maxqt { get; set; }

        [JsonProperty("mlt.mintf")]
        public int mintf { get; set; }

        [JsonProperty("mlt.fl")]
        public string fl { get; set; }

        public override string GetQuery()
        {
            return string.Format("mlt.interestingTerms={0}&mlt.mindf={1}&mlt.minwl={2}&maxqt={3}&mintf={4}", interestingTerms, mindf, minwl, maxqt, mintf);
        }

    }
}