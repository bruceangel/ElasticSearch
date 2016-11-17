using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXXK_Index.Model
{
    /// <summary>
    /// 作用：索引类型配置类
    /// 作者：刘成
    /// 编写日期：2016/11/17 17:45:09;
    /// </summary>
    public class ElasticConfig
    {
        /// <summary>
        /// 数据库表名称
        /// </summary>
        public string DataBaseTableName { get; set; }

        /// <summary>
        /// 一次获取表数据容量
        /// </summary>
        public int GetTableDataPageSize { get; set; }

        /// <summary>
        /// 是否创建子类型（true:创建子类型；false:创建正常类型）
        /// </summary>
        public bool IsCreateChildType { get; set; }

        /// <summary>
        /// 创建索引名称
        /// </summary>
        public string CreateIndexName { get; set; }

        /// <summary>
        /// 创建类型名称
        /// </summary>
        public string CreateTypeName { get; set; }
    }
}
