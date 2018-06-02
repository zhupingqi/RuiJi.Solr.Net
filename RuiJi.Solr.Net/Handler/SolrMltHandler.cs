using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net.Handler
{
    /// <summary>
    /// 相似结果处理器
    /// </summary>
    public class SolrMltHandler : SolrHandlerBase<SolrSelectRequest>
    {
        public SolrMltHandler(SolrConnection connection)
            : base(connection)
        {

        }

        public async Task<SolrResponse> Request(SolrSelectRequest request, string shard)
        {
            var url = string.Format(RelativeUrl, shard);

            return await connection.Get(url, request.GetQuery()).ConfigureAwait(false);
        }

        public override string SetRequestUrl()
        {
            return "/mlt";
        }
    }
}