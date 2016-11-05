using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXXK_Index.Function
{
    /// <summary>
    /// 作用：ES帮助类
    /// 作者：刘成
    /// 编写日期：2016/11/4 11:41:25;
    /// </summary>
    public class ElasticSearchHelper
    {        
        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="indexName">索引名称</param>
        public ElasticSearchHelper(string indexName=null)
        {
            //创建ES连接
            var node = new Uri(Config.GetElasticSearchUrl);
            ConnectionSettings settings;
            if (string.IsNullOrEmpty(indexName))//不带索引
            {
                settings = new ConnectionSettings(node);
            }
            else//带索引
            {
                settings = new ConnectionSettings(node).DefaultIndex(indexName);
            }
            _client = new ElasticClient(settings);
        }

        //ES客户端
        private IElasticClient _client;
        public IElasticClient Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }
    }
}
