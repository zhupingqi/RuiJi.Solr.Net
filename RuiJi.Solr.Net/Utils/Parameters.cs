using Regards.Solr.Net.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net.Utils
{
    public class Parameters
    {
        //public IEnumerable<KeyValuePair<string, string>> GetCommonParameters(CommonQueryOptions options)
        //{
        //    if (options == null)
        //        yield break;

        //    if (options.Start.HasValue)
        //        yield return KV.Create("start", options.Start.ToString());

        //    var rows = options.Rows.HasValue ? options.Rows.Value : DefaultRows;
        //    yield return KV.Create("rows", rows.ToString());

        //    if (options.Fields != null && options.Fields.Count > 0)
        //        yield return KV.Create("fl", string.Join(",", options.Fields.ToArray()));

        //    foreach (var p in GetFilterQueries(options.FilterQueries))
        //        yield return p;

        //    foreach (var p in GetFacetFieldOptions(options.Facet))
        //        yield return p;

        //    if (options.ExtraParams != null)
        //        foreach (var p in options.ExtraParams)
        //            yield return p;
        //}

        //public static IEnumerable<KeyValuePair<string, string>> GetAllParameters(ISolrQuery Query, QueryOptions options)
        //{
        //    yield return KV.Create("q", querySerializer.Serialize(Query));
        //    if (options == null)
        //        yield break;

        //    foreach (var p in GetCommonParameters(options))
        //        yield return p;

        //    if (options.OrderBy != null && options.OrderBy.Count > 0)
        //        yield return KV.Create("sort", string.Join(",", options.OrderBy.Select(x => x.ToString()).ToArray()));

        //    foreach (var p in GetTermsParameters(options))
        //        yield return p;

        //    foreach (var p in GetTermVectorQueryOptions(options))
        //        yield return p;

        //    foreach (var p in GetHighLightParameters(options.HighLight))
        //        yield return p;
        //}
    }
}
