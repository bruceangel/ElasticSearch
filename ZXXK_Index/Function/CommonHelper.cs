using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ZXXK_Index.Function
{
    /// <summary>
    /// 作用：
    /// 作者：刘成
    /// 编写日期：2016/11/17 18:10:14;
    /// </summary>
    public class CommonHelper<T> where T : new()
    {
        /// <summary>
        /// 读取传入的文件路径，并转换为集合
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<T> GetListByFilePath(string fullPath)
        {
            string config = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<List<T>>(config);
        }
    }
}
