using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net
{
    public interface ISolrHandler
    {
        
    }

    public interface ISolrHandler<T> : ISolrHandler
    {
        SolrConnection connection { get; }

        Task<SolrResponse> Request(T request);
    }
}
