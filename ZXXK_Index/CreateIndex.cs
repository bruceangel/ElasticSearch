using Elasticsearch.Net;
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
        //ES客户端
        private IElasticClient eClient;

        //业务类
        private SoftDal tSoft = new SoftDal();
        private ConsumeLogDal tConsumeLog = new ConsumeLogDal();
        #endregion

        public CreateIndex()
        {
            InitializeComponent();
        }

        #region 创建索引数据
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
                WriteLog("创建索引开始...");

                //获取ES客户端对象
                eClient = new ElasticSearchHelper(_indexName).Client;

                //开启线程
                runThread = new Thread(new ThreadStart(CreateIndexData));
                runThread.Start();
            }
        }

        /// <summary>
        /// 创建索引数据
        /// </summary>
        private void CreateIndexData()
        {
            //获取表数据分页参数
            BaseModel model = new BaseModel();
            model.PageSize = _pageSize;

            //总（总个数和当前个数）
            int sumTotalParent = tSoft.GetSoftCount();//获取表总数量
            int curTotalParent = 0;
            if (sumTotalParent <= 0)
            {
                ConsoleWriteResult("查找表" + _tableName + "数据为空！");
                return;
            }

            //循环次数
            int loopCount = (int)Math.Ceiling(sumTotalParent * 1.0 / _pageSize);
            List<Soft> softList = null;
            try
            {
                for (int i = 1; i <= loopCount; i++)
                {
                    model.PageIndex = i;
                    softList = tSoft.GetSoftList(model);
                    if (softList != null && softList.Count > 0)
                    {
                        //方式1：写入索引
                        //WriteIndex(sumTotalParent, softList, ref curTotalParent);

                        //方式2：写入索引
                        curTotalParent += softList.Count;
                        //写入索引
                        eClient.IndexMany(softList);
                        //进度显示
                        SetTextMesssageAll(100 * curTotalParent / sumTotalParent, sumTotalParent, curTotalParent);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("线程意外终止:" + ex.StackTrace);
                runThread.Interrupt();//结束线程
                return;
            }
            runThread.Interrupt();//结束线程
            //runThread.Join();////使调用线程runThread在此之前执行完毕。t.join(1000); //等待 t 线程，等待时间是1000毫秒；
        }

        /// <summary>
        /// 写入索引
        /// </summary>
        /// <param name="totalNumber">表总个数</param>
        /// <param name="softList">单次资料集合</param>
        /// <returns></returns>
        private void WriteIndex(int sumTotalParent, List<Soft> softList, ref int curTotalParent)
        {
            //子（总个数和当前个数）            
            int sumTotalChild = softList.Count;//单次总个数
            int curTotalChild = 0;//单次累加个数

            //方法一：单个插入
            //for (int i = 0; i < sumTotalChild; i++)
            //{
            //    curTotalChild++;
            //    curTotalParent++;
            //    //写入索引
            //    eClient.Index(softList[i]);
            //    //进度显示
            //    SetTextMesssage(100 * (i + 1) / sumTotalChild, "总索引：" + curTotalParent + "   子索引：" + curTotalChild + "   ID：" + softList[i].SoftID, sumTotalChild, i + 1);
            //    SetTextMesssageAll(100 * curTotalParent / sumTotalParent, sumTotalParent, curTotalParent);
            //}

            //方法二：批量插入
            int baseNum = 300;//一次插入索引中的数量
            int sumTotalSecond = (int)Math.Ceiling(sumTotalChild * 1.0 / baseNum);
            int currNum = 0;
            for (int i = 0; i < sumTotalSecond; i++)
            {
                int resultListCount = 0;
                //写入索引
                eClient.IndexMany(PartialSoftList(softList, baseNum, ref currNum, out resultListCount));
                curTotalChild += resultListCount;
                curTotalParent += resultListCount;
                //进度显示
                SetTextMesssage(100 * (i * baseNum + resultListCount) / sumTotalChild, "总索引：" + curTotalParent + "   子索引：" + curTotalChild + "   IDCounts：" + resultListCount, sumTotalChild, i * baseNum + resultListCount);
                SetTextMesssageAll(100 * curTotalParent / sumTotalParent, sumTotalParent, curTotalParent);
            }
        }

        /// <summary>
        /// 子进度二次封装
        /// </summary>
        /// <param name="softList"></param>
        /// <param name="baseNum"></param>
        /// <param name="currNum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private List<Soft> PartialSoftList(List<Soft> softList, int baseNum, ref int currNum, out int num)
        {
            num = 0;
            int count = softList.Count();
            List<Soft> list = new List<Soft>();
            for (int i = 0; i < baseNum; i++)
            {
                if (count > currNum)
                {
                    list.Add(softList[currNum]);
                    currNum++;
                }
                else
                {
                    break;
                }

            }
            num = list.Count();
            return list;
        }
        #endregion

        #region 创建索引数据-子
        /// <summary>
        /// 创建子索引数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCreateChildIndex_Click(object sender, EventArgs e)
        {
            //校验          
            if (CheckValue())
            {
                WriteLog("创建索引开始...");

                //获取ES客户端对象
                eClient = new ElasticSearchHelper(_indexName).Client;

                //开启线程
                runThread = new Thread(new ThreadStart(CreateChildIndexData));
                runThread.Start();
            }            
        }

        /// <summary>
        /// 创建子索引数据
        /// </summary>
        private void CreateChildIndexData()
        {
            //获取表数据分页参数
            BaseModel model = new BaseModel();
            model.PageSize = _pageSize;

            //总（总个数和当前个数）
            int sumTotalParent = tConsumeLog.GetConsumeLogCount();//获取表总数量
            int curTotalParent = 0;
            if (sumTotalParent <= 0)
            {
                ConsoleWriteResult("查找表" + _tableName + "数据为空！");
                return;
            }

            //循环次数
            int loopCount = (int)Math.Ceiling(sumTotalParent * 1.0 / _pageSize);
            List<ConsumeLog> list = null;
            try
            {
                for (int i = 1; i <= loopCount; i++)
                {
                    model.PageIndex = i;
                    list = tConsumeLog.GetConsumeLogList(model);
                    if (list != null && list.Count > 0)
                    {
                        //写入索引
                        WriteChildIndex(sumTotalParent, list, ref curTotalParent);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("线程意外终止:" + ex.StackTrace);
                runThread.Interrupt();//结束线程
                return;
            }
            runThread.Interrupt();//结束线程
            //runThread.Join();////使调用线程runThread在此之前执行完毕。t.join(1000); //等待 t 线程，等待时间是1000毫秒；
        }

        /// <summary>
        /// 写入索引
        /// </summary>
        /// <param name="totalNumber">表总个数</param>
        /// <param name="list">单次集合</param>
        /// <returns></returns>
        private void WriteChildIndex(int sumTotalParent, List<ConsumeLog> list, ref int curTotalParent)
        {
            //子（总个数和当前个数）            
            int sumTotalChild = list.Count;//单次总个数
            int curTotalChild = 0;//单次累加个数

            //单个插入
            for (int i = 0; i < sumTotalChild; i++)
            {
                curTotalChild++;
                curTotalParent++;
                //写入索引
                //eClient.Index(list[i]);
                eClient.Index(list[i],x=>x.Index(_indexName).Parent(list[i].InfoID.ToString()));
                //进度显示
                SetTextMesssage(100 * (i + 1) / sumTotalChild, "总索引：" + curTotalParent + "   子索引：" + curTotalChild + "   ID：" + list[i].ID, sumTotalChild, i + 1);
                SetTextMesssageAll(100 * curTotalParent / sumTotalParent, sumTotalParent, curTotalParent);
            }
        }

        #endregion

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
        /// 获取并校验创建索引需要输入的值
        /// </summary>
        private bool CheckValue()
        {
            _tableName = this.txtTableName.Text;
            if (string.IsNullOrEmpty(_tableName.Trim()))
            {
                MessageBox.Show("请输入要获取数据库的表名称！");
                return false;
            }

            string pageSize = this.txtPageContainer.Text;
            if (!int.TryParse(pageSize, out _pageSize))
            {
                MessageBox.Show("容量输入错误，请重新输入正确的容量数！");
                return false;
            }

            _indexName = this.txtIndexName.Text;
            if (string.IsNullOrEmpty(_indexName.Trim()))
            {
                MessageBox.Show("请输入要创建索引的名称！");
                return false;
            }
            return true;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelIndex_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string indexName = this.txtDelIndexName.Text.Trim();
            if (string.IsNullOrEmpty(this.txtDelIndexName.Text.Trim()))
            {
                MessageBox.Show("请输入要删除的索引名称！");
                return;
            }
            string typeName = this.txtDelTypeName.Text.Trim();
            try
            {
                //获取ES客户端对象
                var client = new ElasticSearchHelper().Client;
                if (!string.IsNullOrEmpty(typeName))//删除类型
                {
                    //client.DeleteByQuery();
                    this.txtResult.AppendText("删除类型功能待完善！");
                }
                else//删除索引
                {
                    var indexResult = client.DeleteIndex(indexName);
                    flag = indexResult.IsValid;
                }
            }
            catch (Exception ex)
            {
                this.txtResult.AppendText("获取ES客户端对象失败：" + ex.StackTrace);
            }

            if (flag)
            {
                MessageBox.Show("删除索引或类型成功！");
            }
            else
            {
                MessageBox.Show("删除索引或类型失败！");
            }
        }

        /// <summary>
        /// 创建zxxk索引（soft\consumelog父子级类型）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParentIndex_Click(object sender, EventArgs e)
        {
            //获取ES客户端对象
            var client = new ElasticSearchHelper().Client;

            //基本配置
            //IIndexState indexState = new IndexState()
            //{
            //    Settings = new IndexSettings()
            //    {
            //        NumberOfReplicas = 1,//副本数
            //        NumberOfShards = 5   //分片数
            //    }
            //};
            ////创建soft\consumelog父子级类型
            //client.CreateIndex("zxxk", p => p.InitializeUsing(indexState)
            //     .Mappings(m => m
            //         .Map<Soft>(s => s.AutoMap())
            //         .Map<ConsumeLog>(c => c.AutoMap().Parent<Soft>())
            //         )
            //    );


            // 根据数据类型生成相应的ES数据类型
            var descriptor = new CreateIndexDescriptor("zxxk")
                .Mappings(map => map
                    .Map<Soft>(tm => tm.AutoMap())
                    .Map<ConsumeLog>(tm => tm.AutoMap().Parent<Soft>())
                );
            var result = client.CreateIndex(descriptor);
            if (result.Acknowledged)
            {
                MessageBox.Show("创建索引zxxk（父类型soft,子类型consumelog）成功！");
            }
            else
            {
                MessageBox.Show("创建索引zxxk（父类型soft,子类型consumelog）失败！");
            }
        }

    }
}
