using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXXK_Index.Model
{
    /// <summary>
    /// 作用：分页基类
    /// 作者：刘成
    /// 编写日期：2016/10/27 11:45:39;
    /// </summary>
    public class BaseModel
    {
        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        private int _pageSize = 10;
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = value;
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }

            set
            {
                _pageCount = value;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 分页开始的条数
        /// </summary>
        public int StartNum { get; set; }

        /// <summary>
        /// 分页结束的条数
        /// </summary>
        public int EndNum { get; set; }
    }
}
