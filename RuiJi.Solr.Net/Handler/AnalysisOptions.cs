using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Regards.Solr.Net;

namespace RuiJi.Net.Handler
{
    public class AnalysisOptions : RequestOptionsBase
    {
        [JsonProperty("_")]
        public string _ { get; set; }

        [JsonProperty("analysis.fieldtype")]
        public string fieldtype { get; set; }

        [JsonProperty("analysis.query")]
        public string query { get; set; }

        [JsonProperty("analysis.showmatch")]
        public string showmatch { get; set; }

        [JsonProperty("verbose_output")]
        public int verbose_output { get; set; }

        public override string GetQuery()
        {
            return string.Format("_={0}&analysis.fieldtype={1}&analysis.query={2}&analysis.showmatch={3}&verbose_output={4}&wt=json", _, fieldtype, query, showmatch, verbose_output);
        }
    }
}
