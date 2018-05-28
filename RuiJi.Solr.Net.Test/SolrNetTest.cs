using Regards.Solr.Net.Handler;
using Regards.Solr.Net.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regards.Solr.Net.Test.Document;
using System.IO;
using RuiJi.Net.Test;
using RuiJi.Net.Handler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Regards.Solr.Net.Test
{
    [TestClass]
    public class SolrNetTest
    {
        [TestMethod]
        static void TestQuery()
        {
            var selectHandler = SolrServerInstance.NewsServer.GetHandler<SolrSelectHandler>();
            selectHandler.Collection = "web";

            var query = new SolrSelectRequest();
            query.q = "*:*";
            query.fq = new List<string>() { "id:160600001623" };
            query.collection = "web,epaper";

            var q = query.GetQuery();

            var response = selectHandler.Request(query);
            response.Wait();

            SolrResponse.AddResponseTypeMap<SolrResponse<WebDocument>>("response");
            var d = response.Result.GetData<SolrResponse<WebDocument>>();
        }

        [TestMethod]
        static void TestMlt()
        {
            var query = new SolrSelectRequest();

            var id = "160800921310";
            var shard = (id.Length == 12 ? "web" : "epaper") + "20" + id.Substring(0, 4);
            var mltHandler = SolrServerInstance.NewsServer.GetHandler<SolrMltHandler>();
            mltHandler.Shard = shard;
            query.q = "id:" + id;
            query.fl = "id,newstime,words";
            query.rows = 1;
            query.collection = "web,epaper";
            var mlt = new MltOptions();
            mlt.interestingTerms = "details";
            mlt.mindf = 2;
            mlt.minwl = 2;
            mlt.maxqt = 50;
            mlt.mintf = 2;

            query.ExtraOptions.Add(mlt);

            var response = mltHandler.Request(query, shard);
            response.Wait();

            var doc = response.Result.GetData<MLTDocument>("match.docs[0]");
            var terms = response.Result.GetData<List<string>>("interestingTerms");

            var newstime = doc.newstime;
            var words = doc.words;

            query = new SolrSelectRequest();
            query.q = ScoreQuery(terms.ToArray());
            query.fl = "id,title,score,media,newstime";
            query.rows = 100;
            query.fq.Add("words:[" + Convert.ToInt32(words * 0.5d) + " TO " + Convert.ToInt32(words * 1.5d) + "]");
            var minDate = newstime.AddMonths(-1).ToString("yyyy-MM-ddT00:00:00Z");
            var maxDate = newstime.AddMonths(1).ToString("yyyy-MM-ddT00:00:00Z");
            query.fq.Add("newstime:[" + minDate + " TO " + maxDate + "]");
            query.sort = "score asc";
            query.collection = "web,epaper";

            var selectHandler = SolrServerInstance.NewsServer.GetHandler<SolrSelectHandler>();
            selectHandler.Collection = "web";
            response = selectHandler.Request(query);

            var mltDocs = response.Result.GetData<List<WebDocument>>("response.docs");

            Assert.IsTrue(mltDocs.Count >= 0);
        }

        [TestMethod]
        static void TestAnalysis()
        {
            string key = "宝马x1";
            var query = new SolrSelectRequest();
            var handler = SolrServerInstance.NewsServer.GetHandler<SolrAnalysisHandler>();
            handler.Collection = "web";
            var analysisOpt = new AnalysisOptions();
            analysisOpt._ = DateTime.Now.ToLongTimeString();
            analysisOpt.fieldtype = "text_complex";
            analysisOpt.query = key;
            analysisOpt.showmatch = "true";
            analysisOpt.verbose_output = 0;

            query.ExtraOptions.Add(analysisOpt);
            var response = handler.Request(query);
            response.Wait();
            var analysis = response.Result.GetData<List<AnalysisField>>("analysis.field_types.text_complex.query[-1:]");

            Assert.IsTrue(analysis.Count >= 0);
        }

        [TestMethod]
        static void TestAddDocument()
        {
            var docs = new List<Document.Document>();

            var updateHandler = SolrServerInstance.NewsServer.GetHandler<SolrUpdateHandler>();
            updateHandler.Collection = "web";
            var request = new SolrUpdateRequest();
            request.RequestMethod = SolrUpdateRequestMethod.Add;
            request.docs.AddRange(docs);

            var response = updateHandler.Request(request);
            response.Wait();

            var d = response.Result.GetData<SolrResponseHeader>("responseHeader");

            Assert.IsTrue(d.Status >= 0);
        }

        [TestMethod]
        static void TestUpdateDocumentTone()
        {
            var ids = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "2.txt");
            var c = 0;

            foreach (var id in ids)
            {
                UpdateDocumentTone(id);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        static void UpdateDocumentTone(string id)
        {
            var updateHandler = SolrServerInstance.ToneServer.GetHandler<SolrUpdateHandler>();
            var request = new SolrUpdateRequest();
            request.RequestMethod = SolrUpdateRequestMethod.Set;
            request.docs.Add(new ToneDocument()
            {
                id = id.ToString(),
                tone = "positive"
            });

            var response = updateHandler.Request(request);
            response.Wait();

            var d = response.Result.GetData<SolrResponseHeader>("responseHeader");

            Assert.IsTrue(d.Status >= 0);
        }

        static string ScoreQuery(string[] terms)
        {
            var boost = new List<string>();

            for (int i = 0; i < terms.Length; i += 2)
            {
                boost.Add(terms[i] + "^" + Convert.ToDouble(terms[i + 1]));
            }

            return String.Join(" AND ", boost);
        }

        [TestMethod]
        static void TestAddTone()
        {
            var updateHandler = SolrServerInstance.ToneServer.GetHandler<SolrUpdateHandler>();
            var request = new SolrUpdateRequest();
            //request.commit = "true";
            request.RequestMethod = SolrUpdateRequestMethod.Add;
            request.docs.Add(new ToneDocument()
            {
                id = "1",
                tone = "negative",
                title = "aa",
                content = "bb"
            });
            request.docs.Add(new ToneDocument()
            {
                id = "2",
                tone = "negative",
                title = "aaaa",
                content = "bbbb"
            });

            var response = updateHandler.Request(request);
            response.Wait();

            var d = response.Result.GetData<SolrResponseHeader>("responseHeader");

            Assert.IsTrue(d.Status >= 0);
        }

        [TestMethod]
        static void TestToneDelete()
        {
            var updateHandler = SolrServerInstance.ToneServer.GetHandler<SolrUpdateHandler>();
            var request = new SolrUpdateRequest();
            request.commit = "true";
            request.RequestMethod = SolrUpdateRequestMethod.Delete;
            request.docs.Add(new ToneDocument()
            {
                id = "1"
            });
            request.docs.Add(new ToneDocument()
            {
                id = "2"
            });

            var response = updateHandler.Request(request);
            response.Wait();

            var d = response.Result.GetData<SolrResponseHeader>("responseHeader");

            Assert.IsTrue(d.Status >= 0);
        }

        [TestMethod]
        static void TestToneCommit()
        {
            var updateHandler = SolrServerInstance.ToneServer.GetHandler<SolrUpdateHandler>();
            var request = new SolrUpdateRequest();

            var response = updateHandler.Commit();
            response.Wait();

            var d = response.Result.GetData<SolrResponseHeader>("responseHeader");

            Assert.IsTrue(d.Status >= 0);
        }
    }
}