using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Solr.Net
{
    public abstract class RequestOptionsBase : IRequestOptions
    {
        public abstract string GetQuery();
    }
}
