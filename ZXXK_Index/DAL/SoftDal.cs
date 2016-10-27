using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using ZXXK_Index.DBUtility;
using ZXXK_Index.Model;

namespace ZXXK_Index.DAL
{
    /// <summary>
    /// 作用：资料数据类
    /// 作者：刘成
    /// 编写日期：2016/10/27 11:44:24;
    /// </summary>
    public class SoftDal
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="eBaseModel"></param>
        /// <returns></returns>
        public List<Soft> GetSoftList(BaseModel eBaseModel)
        {
            string sql = @"select * from
                            (
	                            select * ,ROW_NUMBER() over(order by SoftID) as RID
	                            from Cl_Soft
                            )as temp
                            where temp.RID between @StartNum and @EndNum";

            eBaseModel.StartNum = (eBaseModel.PageIndex - 1) * eBaseModel.PageSize + 1;
            eBaseModel.EndNum = eBaseModel.PageIndex * eBaseModel.PageSize;

            List<Soft> list = null;
            using (IDbConnection conn = DBConns.GetConnection(DBs.Zxxk))
            {
                list = conn.Query<Soft>(sql, eBaseModel).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取资料总个数
        /// </summary>
        /// <returns></returns>
        public int GetSoftCount()
        {
            string sql = "select count(1)as Counts from Cl_Soft";

            int counts = 0;
            using (IDbConnection conn=DBConns.GetConnection(DBs.Zxxk))
            {
                counts =Convert.ToInt32(conn.ExecuteScalar(sql));
            }
            return counts;
        }
    }
}
