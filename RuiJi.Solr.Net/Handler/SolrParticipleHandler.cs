using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net.Handler
{
    /// <summary>
    /// 分词统计请求处理器（此Solr接口需要单独扩展编写）
    /// </summary>
    public class SolrParticipleHandler : SolrHandlerBase<SolrParticipleRequest>
    {
        public SolrParticipleHandler(SolrConnection connection)
            : base(connection)
        {

        }

        public override string SetRequestUrl()
        {
            return "/tquery";
        }
    }
}
