using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXXK_Index.Function
{
    public static class Config
    {
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }

        /// <summary>
        /// 获取ES服务路径
        /// </summary>
        /// <returns></returns>
        public static string GetElasticSearchUrl
        {
            get
            {
                return GetAppSettings("ElasticSearchUrl");
            }
        }
    }
}
