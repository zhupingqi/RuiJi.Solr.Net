using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Test.Document
{
    public abstract class DocumentBase
    {
        public string id { get; set; }
        public string title { get; set; }
        [JsonConverter(typeof(SolrDateTimeConverter))]
        public DateTime newstime { get; set; }
        [JsonConverter(typeof(SolrDateTimeConverter))]
        public DateTime addtime { get; set; }
        public string media { get; set; }
        public string sourcemedia { get; set; }
        public int mediatype { get; set; }
        public string _route_
        {
            get
            {
                return mediatype == 1 ? "epaper" + addtime.ToString("yyyyMM") : "web" + addtime.ToString("yyyyMM");
            }
        }
        public string url { get; set; }
        public string content { get; set; }
        public string reporter { get; set; }
        public int words { get; set; }
        public string tone { get; set; }
    }
}