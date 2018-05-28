using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net
{
    public class SolrServer
    {
        private SolrConnection connection;
        public List<Type> Handlers
        {
            get;
            private set;
        }

        public SolrServer(SolrConnection connection)
        {
            this.connection = connection;
            Handlers = new List<Type>();
        }

        public void AddHandler<T>() where T : ISolrHandler
        {
            Handlers.Add(typeof(T));
        }

        private ISolrHandler CreateHandler(Type t, object[] parmas)
        {
            return Activator.CreateInstance(t, parmas) as ISolrHandler;
        }

        public T GetHandler<T>() where T : ISolrHandler
        {
            return (T)CreateHandler(typeof(T), new object[] { connection });
        }

        public async Task<SolrResponse> Commit()
        {
            return await connection.Get("/commit","").ConfigureAwait(false);
        }
    }
}