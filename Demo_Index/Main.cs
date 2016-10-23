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
            for (int i = 0; i < 100; i++)
            {
                info = new UserInfo();
                info.UserID = i + 1;
                info.UserName = "笑傲江湖" + i;
                info.Age = 2 * i;
                info.AddTime = DateTime.Now;
                info.Year = 2016;
                info.IsDeleted = 0;
                list.Add(info);
            }

            UserInfo userinfo = new UserInfo();
            userinfo.UserID = 1;
            userinfo.UserName = "笑傲江湖";
            userinfo.Age = 23;
            userinfo.AddTime = DateTime.Now;
            userinfo.Year = 2016;
            userinfo.IsDeleted = 0;

            var result = client.Index(userinfo, o => o.Index("zxxk"));
           
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
    }

    [ElasticsearchType(IdProperty = "UserID")]
    public class UserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime AddTime { get; set; }
        public int Year { get; set; }
        public int IsDeleted { get; set; }
    }
}
