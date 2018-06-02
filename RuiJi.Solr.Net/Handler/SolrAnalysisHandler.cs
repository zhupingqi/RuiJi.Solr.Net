using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Handler
{
    public class SolrAnalysisHandler : SolrHandlerBase<SolrSelectRequest>
    {
        public SolrAnalysisHandler(SolrConnection connection)
            : base(connection)
        {

        }
        public override string SetRequestUrl()
        {
            return "/analysis/field";
        }
    }
}
