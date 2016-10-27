using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZXXK_Index.DBUtility
{
    /// <summary>
    /// 作用：连接数据库
    /// 作者：刘成
    /// 编写日期：2016/10/27 10:02:55;
    /// </summary>
    public static class DBConns
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="db">枚举</param>
        /// <returns></returns>
        private static string GetConnStr(DBs db)
        {
            string conn = string.Empty;
            switch (db)
            {
                case DBs.Zxxk:
                    conn = ConfigurationManager.ConnectionStrings["Zxxk"].ConnectionString;
                    break;
                default:
                    conn = ConfigurationManager.ConnectionStrings["Zxxk"].ConnectionString;
                    break;
            }
            return conn;
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IDbConnection GetConnection(DBs db)
        {
            string connStr = GetConnStr(db);
            return new SqlConnection(connStr);
        }

    }

    /// <summary>
    /// 数据库连接库枚举
    /// </summary>
    public enum DBs
    {
        Zxxk = 1  //主库
    }
}
