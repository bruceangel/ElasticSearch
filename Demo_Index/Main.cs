using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Index
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 创建索引-单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            //单node
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            //插入
            List<UserInfo> list = new List<UserInfo>();
            UserInfo info = null;
            for (int i = 1; i <= 100; i++)
            {
                info = new UserInfo();
                info.UserID = i ;
                info.UserName = "笑傲江湖" + i;
                info.Age = 2 * i;
                info.AddTime = DateTime.Now;
                info.Year = 2016;
                info.IsDeleted = 0;
                list.Add(info);               
            }

            //client.IndexMany(list, index: "test1");
            client.IndexMany(list, index: "test2");
            //client.IndexMany(list,index:"test-index3");

            this.txtResult.Text = "[创建索引-单]成功！";
        }

        /// <summary>
        /// 创建索引-多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateIndex2_Click(object sender, EventArgs e)
        {
            //多uri
            var uris = new Uri[] {
                new Uri("http://localhost:9200")
            };
            //多node
            var nodes = new Node[] {
                new Node (new Uri("http://localhost:9200")),
                new Node (new Uri("http://localhost:9200"))
            };

            #region 连接池类型
            ////对单节点请求
            //IConnectionPool pool1 = new SingleNodeConnectionPool(uris.FirstOrDefault());

            ////请求时随机请求各个正常节点，不请求异常节点,异常节点恢复后会重新被请求
            //IConnectionPool pool2 = new StaticConnectionPool(uris);

            //IConnectionPool pool3 = new SniffingConnectionPool(uris);
            ////false.创建客户端时，随机选择一个节点作为客户端的请求对象，该节点异常后不会切换其它节点
            ////true，请求时随机请求各个正常节点，不请求异常节点,但异常节点恢复后不会重新被请求
            //pool3.SniffedOnStartup = true;

            ////创建客户端时，选择第一个节点作为请求主节点，该节点异常后会切换其它节点，待主节点恢复后会自动切换回来
            //IConnectionPool pool4 = new StickyConnectionPool(uris);
            #endregion
            var pool = new StaticConnectionPool(uris);    //第一种连接池
            //var pool = new StaticConnectionPool(nodes); //第二种连接池                                 
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            UserInfo userinfo = new UserInfo();
            userinfo.UserID = 0;
            userinfo.UserName = "笑傲江湖0";
            userinfo.Age = 6;
            userinfo.AddTime = DateTime.Now;
            userinfo.Year = 2016;
            userinfo.IsDeleted = 0;
            var result = client.Index(userinfo, o => o.Index("zxxk"));

            this.txtResult.Text = "[创建索引-多]成功！";
        }

        /// <summary>
        /// 只创建索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateIndex3_Click(object sender, EventArgs e)
        {
            //单node
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            //创建1：空索引
            client.CreateIndex("test1");

            //基本配置
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 1,//副本数
                    NumberOfShards = 5   //分片数
                }
            };
            //创建2：带配置
            client.CreateIndex("test2", p => p.InitializeUsing(indexState));
            //创建3：创建并Mapping
            client.CreateIndex("test-index3", p => p.InitializeUsing(indexState).Mappings(m => m.Map<UserInfo>(mp => mp.AutoMap())));

            this.txtResult.Text = "[只创建索引]成功！";
        }

        /// <summary>
        /// 查询1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch1_Click(object sender, EventArgs e)
        {
            var node = new Uri("http://localhost:9200");            
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            Indices index = "test";
            //查询
            var searchResult = client.Search<UserInfo>(s => s.Index(index));
        }

        /// <summary>
        /// 删除-范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted1_Click(object sender, EventArgs e)
        {
            //单node
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            //var settings = new ConnectionSettings(node).DefaultIndex("zxxk");//不推荐
            var client = new ElasticClient(settings);

            Indices indices = "test2";
            Types types = "userinfo";
            //批量删除 需要es安装 delete-by-query插件
            var result = client.DeleteByQuery<UserInfo>(indices, types,
                dq =>
                    dq.Query(
                        q =>
                            q.TermRange(tr => tr.Field(fd => fd.Age).GreaterThanOrEquals("20").LessThanOrEquals("198")))
                            );
            if (result.IsValid)
            {
                this.txtResult.Text = "[删除-范围]成功！";
            }
            else
            {
                this.txtResult.Text = "[删除-范围]失败！";
            }
        }

        /// <summary>
        /// 删除-索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted2_Click(object sender, EventArgs e)
        {
            //单node
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            //索引test是否存在
            if (client.IndexExists("test").Exists)
            {
                client.DeleteIndex("test");
            }

            client.DeleteIndex("test2");

            this.txtResult.Text = "[删除-索引]成功！";
        }

        /// <summary>
        /// 删除-唯一ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleted3_Click(object sender, EventArgs e)
        {
            //单node
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            client.Delete<UserInfo>(new UserInfo() { UserID = 100 }, o => o.Index("test2"));

            DocumentPath<UserInfo> deletePath = new DocumentPath<UserInfo>(1);
            client.Delete(deletePath,o=>o.Index("test1"));

            IDeleteRequest request = new DeleteRequest("test1", "userinfo", 2);
            client.Delete(request);

            this.txtResult.Text = "[删除-唯一ID]成功！";
        }

      
    }

    /// <summary>
    /// 索引唯一Id
    /// </summary>
    [ElasticsearchType(IdProperty = "UserID")]
    public class UserInfo
    {
        [Number(NumberType.Long)]
        public int UserID { get; set; }
        /// <summary>   
        /// 设置索引时字段的名称
        /// </summary>
        [String(Name = "Name")]
        public string UserName { get; set; }
        [String(Name="Age")]
        public int Age { get; set; }
        [Date(Format="yyyy-MM-dd")]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 如果string 类型的字段不需要被分析器拆分，要作为一个正体进行查询，需标记此声明，
        /// 否则索引的值将被分析器拆分.
        /// </summary>
        [String(Index=FieldIndexOption.NotAnalyzed)]
        public string Description { get; set; }
        public int Year { get; set; }        
        public int IsDeleted { get; set; }
        /// <summary>
        /// 如需使用坐标点类型需添加坐标点特性，在maping时会自动映射类型
        /// </summary>
        [GeoPoint(Name = "ZuoBiao", LatLon = true)]
        public GeoLocation Location { get; set; }
    }
}
