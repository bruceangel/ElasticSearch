using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXXK_ElasticSearchWeb.Function;
using ZXXK_ElasticSearchWeb.Model;
using Newtonsoft.Json;

namespace ZXXK_ElasticSearchWeb.Controllers
{
    public class HomeController : Controller
    {
        //ES客户端
        IElasticClient client = null;

        // GET: Home
        public ActionResult Index()
        {
            GetElasticSearchService("zxxk");//获取ES服务

            Search();

            return View();
        }

        /// <summary>
        /// 获取ES服务
        /// </summary>
        /// <param name="indexName">索引名称</param>
        private void GetElasticSearchService(string indexName)
        {
            //连接ES服务
            var node = new Uri(Config.GetElasticSearchUrl);
            var settings = new ConnectionSettings(node).DefaultIndex(indexName);
            client = new ElasticClient(settings);
        }

        private void Search()
        {
            var result1 = client.Get(new DocumentPath<Soft>(5102027));
            var result2 = client.MultiGet(x => x.GetMany<Soft>(new List<long> { 5102076, 5067230 }));
            var result3 = client.Search<Soft>(s => s
                //.From(1)//跳过的数据个数
                .Size(2)//返回数据个数
                .Sort(st => st.Ascending(asc => asc.SoftID))//排序
                .Source(sc => sc.Include(ic => ic.Fields(
                        fd => fd.SoftID,
                        fd => fd.SoftName,
                        fd => fd.AddTime
                    )))//返回特定的字段
                .Explain()//参数可以提供查询的更多详情
                .FielddataFields(ff => ff
                    .Field(f => f.SoftName)
                    .Field(f => f.Intro)
                    )//对指定字段进行分析
                .Query(q =>
                    q.MatchAll()
                    )//条件过滤
                );

            ViewData["result1"] = JsonConvert.SerializeObject(result1);
            ViewData["result2"] = JsonConvert.SerializeObject(result2);
            ViewData["result3"] = JsonConvert.SerializeObject(result3.Documents);

            var result4 = client.Search<Soft>(s => s
                .From(0)
                .Size(15)
                .Aggregations(ag => ag
                    .ValueCount("Count", vc => vc.Field(f => f.SoftPoint))//总数
                    .Sum("Sum", vc => vc.Field(f => f.SoftPoint))//求和
                    .Max("Max", vc => vc.Field(f => f.SoftPoint))//最大值
                    .Min("Min", vc => vc.Field(f => f.SoftPoint))//最小值
                    .Average("Avg", vc => vc.Field(f => f.SoftPoint))//平均值
                    .Terms("Passed_Group", vc => vc.Field(f => f.Passed).Size(100))//分组
                    .Cardinality("Passed_Group_Count", vc => vc.Field(f => f.Passed))//分组数量
                    )//聚合-基本-分组
                );

            ViewData["result4"] = JsonConvert.SerializeObject(result4.Aggregations);

        }

        //************************************************************************************

        public ActionResult LevelIndex()
        {
            GetElasticSearchService("zxxk");//获取ES服务

            SearchLevle();

            return View();
        }

        public void SearchLevle()
        {
            var result1 = client.Search<Soft>(s => s
              .Query(q => q
                .HasChild<ConsumeLog>(hc => hc.Type("consumelog"))
              
              )
            );

        }
    }
}