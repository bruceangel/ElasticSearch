using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ZXXK_Index.Model;
using System.Data;
using ZXXK_Index.DBUtility;

namespace ZXXK_Index.DAL
{
    /// <summary>
    /// 作用：下载资料日志数据类
    /// 作者：刘成
    /// 编写日期：2016/11/11 15:15:05;
    /// </summary>
    public class ConsumeLogDal
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="eBaseModel"></param>
        /// <returns></returns>
        public List<ConsumeLog> GetConsumeLogList(BaseModel eBaseModel)
        {
            string sql = @"select * from
                            (
	                            select * ,ROW_NUMBER() over(order by ID) as RID
	                            from Cl_ConsumeLog 
                            )as temp
                            where temp.RID between @StartNum and @EndNum";

            eBaseModel.StartNum = (eBaseModel.PageIndex - 1) * eBaseModel.PageSize + 1;
            eBaseModel.EndNum = eBaseModel.PageIndex * eBaseModel.PageSize;

            List<ConsumeLog> list = null;
            using (IDbConnection conn = DBConns.GetConnection(DBs.Zxxk))
            {
                list = conn.Query<ConsumeLog>(sql, eBaseModel).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取总个数
        /// </summary>
        /// <returns></returns>
        public int GetConsumeLogCount()
        {
            string sql = "select count(1)as Counts from Cl_ConsumeLog";

            int counts = 0;
            using (IDbConnection conn = DBConns.GetConnection(DBs.Zxxk))
            {
                counts = Convert.ToInt32(conn.ExecuteScalar(sql));
            }
            return counts;
        }
    }
}
