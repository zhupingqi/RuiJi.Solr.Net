using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RuiJi.Solr.Net.Test.Document
{
    public class EPaperDocument : DocumentBase
    {
        public string page { get; set; }
    }

    public enum ToneEnum
    {
        Positive,
        Negative
    }
}