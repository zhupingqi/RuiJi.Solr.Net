using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net
{
    public abstract class SolrHandlerBase<T> : ISolrHandler<T> where T : ISolrRequest
    {
        public SolrConnection connection
        {
            get;
            protected set;
        }

        public string Collection { get; set; }

        public string Shard { get; set; }

        public string RequestUrl { get; set; }

        public string RelativeUrl
        {
            get
            {
                var url = "";
                if (!string.IsNullOrEmpty(Collection))
                    url += "/" + Collection;
                if (!string.IsNullOrEmpty(Shard))
                    url += "/" + Shard;
                if (string.IsNullOrEmpty(RequestUrl))
                    throw new ArgumentException("request url is empty");
                url += RequestUrl;

                return url;
            }
        }

        public SolrHandlerBase(SolrConnection connection)
        {
            this.connection = connection;

            RequestUrl = SetRequestUrl();
        }

        public abstract string SetRequestUrl();

        public virtual async Task<SolrResponse> Request(T request)
        {
            return await connection.Get(RelativeUrl, request.GetQuery()).ConfigureAwait(false);
        }
    }
}