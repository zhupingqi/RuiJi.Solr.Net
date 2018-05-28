using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Regards.Solr.Net.Handler
{
    public class HighlightOptions : RequestOptionsBase
    {
        public string hl { get; set; }

        [JsonProperty("hl.fl")]
        public string Fields { get; set; }

        [JsonProperty("hl.simple.pre")]
        public string Pre { get; set; }

        [JsonProperty("hl.simple.post")]
        public string Post { get; set; }

        public string requireFieldMatch { get; set; }

        public string usePhraseHighlighter { get; set; }

        public string highlightMultiTerm { get; set; }

        public HighlightOptions(string fields, string pre = "", string post = "")
        {
            this.hl = "on";
            this.Fields = fields;
            this.Pre = pre;
            this.Post = post;
        }

        public override string GetQuery()
        {
            var result = string.Format("hl=on&hl.fl={0}", Fields);
            if (!string.IsNullOrEmpty(Pre))
                result = string.Format(result + "&hl.simple.pre={0}&hl.simple.post={2}", Pre);
            if (!string.IsNullOrEmpty(Post))
                result = string.Format(result + "&hl.simple.post={0}", Post);

            return result;
        }
    }
}