using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net.Handler
{
    /// <summary>
    /// 更新对象处理器
    /// </summary>
    public class SolrUpdateHandler : SolrHandlerBase<SolrUpdateRequest>
    {
        public string UniqueField
        {
            get;
            set;
        }

        public SolrUpdateHandler(SolrConnection connection)
            : base(connection)
        {
            UniqueField = "id,_route_";
        }

        public override string SetRequestUrl()
        {
            return "/update";
        }

        public async Task<SolrResponse> Request(SolrUpdateRequest request, ISerializer serializer = null)
        {
            var query = request.GetQuery();

            switch (request.RequestMethod)
            {
                case SolrUpdateRequestMethod.Add:
                    {
                        return await connection.Post(RelativeUrl, query, request.docs).ConfigureAwait(false);
                    }
                case SolrUpdateRequestMethod.Set:
                    {
                        if (serializer == null)
                            serializer = new UpdateSetSerializer(UniqueField);

                        return await connection.Post(RelativeUrl, query, request.docs, serializer).ConfigureAwait(false);
                    }
                case SolrUpdateRequestMethod.Delete:
                    {
                        var ids = new List<string>();

                        foreach (var doc in request.docs)
                        {
                            JObject jObj = JObject.FromObject(doc);
                            var id = jObj.GetValue(UniqueField).ToString();
                            ids.Add(id);
                        }

                        return await connection.Post(RelativeUrl, query, new { delete = ids }).ConfigureAwait(false);
                    }
                case SolrUpdateRequestMethod.Commit:
                    {
                        return await connection.Post(RelativeUrl, "", new { commit = new { } }).ConfigureAwait(false);
                    }
            }

            return null;
        }

        public async Task<SolrResponse> Commit()
        {
            var request = new SolrUpdateRequest();
            request.RequestMethod = SolrUpdateRequestMethod.Commit;
            return await Request(request).ConfigureAwait(false);
        }
    }
}