using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RuiJi.Solr.Net
{
    public class SolrConnection
    {
        public string serverUrl { get; private set; }
        private string version = "2.2";

        /// <summary>
        /// Manages HTTP connection with Solr
        /// </summary>
        /// <param name="serverURL">URL to Solr</param>
        public SolrConnection(string serverURL)
        {
            ServerURL = serverURL;
            Timeout = -1;
        }

        /// <summary>
        /// URL to Solr
        /// </summary>
        public string ServerURL
        {
            get { return serverUrl; }
            set
            {
                serverUrl = value;
            }
        }

        /// <summary>
        /// Solr XML response syntax version
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// HTTP connection timeout
        /// </summary>
        public int Timeout { get; set; }

        public async Task<SolrResponse> Post(string relativeUrl, string query, object json)
        {
            query = "?" + query + "&wt=json&_=" + DateTime.Now.Ticks;

            var request = new RestRequest(relativeUrl + query);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            request.JsonSerializer = new JsonNetSerialzier();
            request.AddBody(json);

            var rest = new RestClient(serverUrl);

            var task = await rest.ExecuteTaskAsync(request).ConfigureAwait(false);

            return new SolrResponse(task.Content);
        }

        public async Task<SolrResponse> Post(string relativeUrl, string query, object json, ISerializer serializer)
        {
            query = "?" + query + "&wt=json&_=" + DateTime.Now.Ticks;

            var request = new RestRequest(relativeUrl + query);
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            request.JsonSerializer = serializer; 
            request.AddBody(json);

            var rest = new RestClient(serverUrl);

            var task = await rest.ExecuteTaskAsync(request).ConfigureAwait(false);

            return new SolrResponse(task.Content);
        }

        public async Task<SolrResponse> Get(string relativeUrl, string query)
        {
            query = "?" + query + "&wt=json&_=" + DateTime.Now.Ticks;

            var request = new RestRequest(relativeUrl + query);
            request.Method = Method.GET;

            var rest = new RestClient(serverUrl);

            var task = await rest.ExecuteTaskAsync(request).ConfigureAwait(false);

            return new SolrResponse(task.Content);
        }
    }
}