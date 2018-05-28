using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net.Handler
{
    public class SolrSelectHandler : SolrHandlerBase<SolrSelectRequest>
    {       
        public SolrSelectHandler(SolrConnection connection)
            : base(connection)
        {
            
        }

        public override string SetRequestUrl()
        {
            return "/select";
        }
    }
}