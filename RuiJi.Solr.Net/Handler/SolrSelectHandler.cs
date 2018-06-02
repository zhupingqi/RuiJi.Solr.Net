using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Handler
{
    /// <summary>
    /// 查询处理器
    /// </summary>
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