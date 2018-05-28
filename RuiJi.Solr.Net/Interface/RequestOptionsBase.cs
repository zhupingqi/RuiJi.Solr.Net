using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net
{
    public abstract class RequestOptionsBase : IRequestOptions
    {
        public abstract string GetQuery();
    }
}
