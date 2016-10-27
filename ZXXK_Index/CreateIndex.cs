using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXXK_Index.DAL;
using ZXXK_Index.Function;
using ZXXK_Index.Model;

namespace ZXXK_Index
{
    public partial class CreateIndex : Form
    {
        #region 业务承载区
        //表名
        private string _tableName = string.Empty;
        //获取表数据容量
        private int _pageSize = 0;
        //索引名称
        private string _indexName = string.Empty;

        //代理
        private delegate void SetPosAll(int ipos, int totalNumber, int curTotal);
        private delegate void SetPos(int ipos, string vinfo, int totalNumber, int curTotal);
        //线程
        private Thread runThread;

        //业务类
        private SoftDal tSoft = new SoftDal();

        #endregion

        public CreateIndex()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCreateIndex_Click(object sender, EventArgs e)
        {
            //校验          
            if (CheckValue())
            {
                //开启线程
                runThread = new Thread(new ThreadStart(CreateIndexData));
                runThread.Start();
            }
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        private void CreateIndexData()
        {
            //创建ES连接
            var node = new Uri(Config.GetElasticSearchUrl);
            var settings = new ConnectionSettings(node).DefaultIndex(_indexName);
            IElasticClient client = new ElasticClient(settings);

            //获取表数据分页参数
            BaseModel model = new BaseModel();
            model.PageSize = _pageSize;

            //获取表总数量
            int totalNumber = tSoft.GetSoftCount();
            if (totalNumber <= 0)
            {
                ConsoleWriteResult("查找表" + _tableName + "数据为空！");
                return;
            }

            //循环次数
            int loopCount = (int)Math.Ceiling(totalNumber / _pageSize * 1.0);
            List<Soft> softList = null;
            try
            {
                for (int i = 1; i <= loopCount; i++)
                {
                    model.PageIndex = i;
                    softList = tSoft.GetSoftList(model);
                    if (softList != null && softList.Count > 0)
                    {
                        //写入索引
                        WriteIndex(totalNumber, softList,client);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("线程意外终止:" + ex.StackTrace);
                runThread.Interrupt();//结束线程
                runThread.Join();
            }
            runThread.Interrupt();//结束线程
            runThread.Join();
        }

        /// <summary>
        /// 写入索引
        /// </summary>
        /// <param name="totalNumber">表总个数</param>
        /// <param name="softList">单次资料集合</param>
        /// <returns></returns>
        private void WriteIndex(int totalNumber, List<Soft> softList, IElasticClient client)
        {
            int curTotal = 0;//单次累加个数
            int sumTotal = softList.Count;//单次总个数

            //方法一
            for (int i = 0; i < sumTotal; i++)
            {
                curTotal++;

                //写入索引
                client.Index(softList);

                //进度显示
                SetTextMesssage(100 * (i + 1) / sumTotal, "索引：" + curTotal + "   ID：" + softList[i].SoftID, sumTotal, i + 1);
                SetTextMesssageAll(100 * curTotal / totalNumber, totalNumber, curTotal);
            }
            
            //方法二


        }

        /// <summary>
        /// 进度显示-总
        /// </summary>
        /// <param name="ipos"></param>
        /// <param name="vinfo"></param>
        /// <param name="totalNumber"></param>
        /// <param name="curTotal"></param>
        private void SetTextMesssageAll(int ipos, int totalNumber, int curTotal)
        {
            if (this.InvokeRequired)
            {
                SetPosAll setposAll = new SetPosAll(SetTextMesssageAll);
                this.Invoke(setposAll, new object[] { ipos, totalNumber, curTotal });
            }
            else
            {
                this.totalNumberAll.Text = totalNumber.ToString();
                this.complateNumberAll.Text = curTotal.ToString();
                this.calcaulatorNumberAll.Text = ipos.ToString() + "%";
                this.progressBarIndexAll.Value = Convert.ToInt32(ipos);
                if (totalNumber == curTotal)
                {
                    WriteLog("所有索引已完成...");
                }                    
            }
        }

        /// <summary>
        /// 进度显示-子
        /// </summary>
        /// <param name="ipos"></param>
        /// <param name="vinfo"></param>
        /// <param name="totalNumber"></param>
        private void SetTextMesssage(int ipos, string vinfo, int totalNumber, int curTotal)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo, totalNumber, curTotal });
            }
            else
            {
                this.totalNumber.Text = totalNumber.ToString();
                this.complateNumber.Text = curTotal.ToString();
                this.calcaulatorNumber.Text = ipos.ToString() + "%";
                this.progressBarIndex.Value = Convert.ToInt32(ipos);

                WriteLog(vinfo);//记录索引日志
                if (totalNumber == curTotal)
                {
                    WriteLog("当前子索引已完成...");
                }
            }
        }

        /// <summary>
        /// 获取并校验界面输入值
        /// </summary>
        private bool CheckValue()
        {
            bool flag = true;
            _tableName = this.txtTableName.Text;
            if (string.IsNullOrEmpty(_tableName.Trim()))
            {
                flag = false;
                MessageBox.Show("请输入要获取数据库的表名称！");
            }

            string pageSize = this.txtPageContainer.Text;
            if (!int.TryParse(pageSize, out _pageSize))
            {
                flag = false;
                MessageBox.Show("容量输入错误，请重新输入正确的容量数！");
            }

            _indexName = this.txtIndexName.Text;
            if (string.IsNullOrEmpty(_indexName.Trim()))
            {
                flag = false;
                MessageBox.Show("请输入要创建索引的名称！");
            }
            return flag;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            //记录日志

            //界面输出结果
            ConsoleWriteResult(msg);
        }

        /// <summary>
        /// 界面输出结果
        /// </summary>
        /// <param name="msg"></param>
        private void ConsoleWriteResult(string msg)
        {
            this.txtResult.AppendText(msg + "\r\n");
        }

    }
}
