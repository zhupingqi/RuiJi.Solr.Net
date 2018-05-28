# RuiJi.Solr.Net
RuiJi.Solr.Net

[![Build status](https://ci.appveyor.com/api/projects/status/q6y0jgq45p0ie5pa/branch/master?svg=true)](https://ci.appveyor.com/project/zhupingqi/ruiji-solr-net/branch/master)

RuiJi.Solr.Net is a apache solr client for .net

The package contains commonly used Solr requests and handler, and can be added on demand.

    var connection = new SolrConnection("http://x.x.x.x:8986/solr");
    var _newsServer = new SolrServer(connection);
    _newsServer.AddHandler<SolrAnalysisHandler>();
    _newsServer.AddHandler<SolrSelectHandler>();
    _newsServer.AddHandler<SolrUpdateHandler>();
    _newsServer.AddHandler<SolrMltHandler>();

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

or
    
    var d = response.Result.GetData<SolrResponse<WebDocument>>("response");
    
support json path

    var doc = response.Result.GetData<MLTDocument>("match.docs[0]");

http://www.ruijihg.com/