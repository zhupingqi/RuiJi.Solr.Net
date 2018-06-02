using Regards.Solr.Net;
using Regards.Solr.Net.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Net.Test
{
    public class SolrServerInstance
    {
        private static SolrServer _toneSolrServer;
        private static SolrServer _newsServer;

        private static SolrServer _toneSolrServer2;
        private static SolrServer _newsServer2;

        static SolrServerInstance()
        {
            var connection = new SolrConnection("http://192.168.101.3:8986/solr");
            _newsServer = new SolrServer(connection);
            _newsServer.AddHandler<SolrAnalysisHandler>();
            _newsServer.AddHandler<SolrSelectHandler>();
            _newsServer.AddHandler<SolrUpdateHandler>();
            _newsServer.AddHandler<SolrMltHandler>();
            _newsServer.AddHandler<SolrParticipleHandler>();


            connection = new SolrConnection("http://192.168.101.3:9000/solr/tone");
            _toneSolrServer = new SolrServer(connection);
            _toneSolrServer.AddHandler<SolrSelectHandler>();
            _toneSolrServer.AddHandler<SolrUpdateHandler>();

            connection = new SolrConnection("http://192.168.101.2:8986/solr");
            _newsServer2 = new SolrServer(connection);
            _newsServer2.AddHandler<SolrAnalysisHandler>();
            _newsServer2.AddHandler<SolrSelectHandler>();
            _newsServer2.AddHandler<SolrUpdateHandler>();
            _newsServer2.AddHandler<SolrMltHandler>();

            connection = new SolrConnection("http://192.168.101.2:9000/solr/tone");
            _toneSolrServer2 = new SolrServer(connection);
            _toneSolrServer2.AddHandler<SolrSelectHandler>();
            _toneSolrServer2.AddHandler<SolrUpdateHandler>();
        }

        public static SolrServer ToneServer
        {
            get
            {
                return _toneSolrServer;
            }
        }

        public static SolrServer NewsServer
        {
            get
            {
                return _newsServer;
            }
        }

        public static SolrServer ToneServer2
        {
            get
            {
                return _toneSolrServer2;
            }
        }

        public static SolrServer NewsServer2
        {
            get
            {
                return _newsServer2;
            }
        }
    }
}
