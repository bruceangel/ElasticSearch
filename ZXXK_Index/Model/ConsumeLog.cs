using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Nest;

namespace ZXXK_Index.Model
{
    /// <summary>
    /// 资料下载日志表实体类
    /// </summary>
    [Serializable]
    [ElasticsearchType(IdProperty = "ID")]
    public class ConsumeLog
    {
        #region Model  
        /// <summary>
        /// 主键ID
        /// </summary>
        [Number(NumberType.Long)]
        public int ID { get; set; }

        /// <summary>
        /// 学科ID
        /// </summary>
        [String(Name = "eSoftID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 资料ID
        /// </summary>
        [String(Name="eSoftID")]
        public int InfoID { get; set; }

        /// <summary>
        /// 资料标题
        /// </summary>
        [String(Name = "eSoftName")]
        public string Title { get; set; }

        /// <summary>
        /// 下载用户ID
        /// </summary>
        [Number(NumberType.Long), String(Name = "eUserID")]
        public int UserID { get; set; }

        /// <summary>
        /// 下载用户名
        /// </summary>
        [String(Name = "eUserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 下载网校通学校ID
        /// </summary>
        [String(Name = "eSchoolUserID")]
        public int SchoolUserID { get; set; }

        /// <summary>
        /// 消费储值
        /// </summary>
        [String(Name = "eConsumeSoftPoint")]
        public decimal ConsumeSoftMoney { set; get; }

        /// <summary>
        /// 消费点数
        /// </summary>
        [String(Name = "eConsumeSoftPoint")]
        public decimal ConsumeSoftPoint { set; get; }

        /// <summary>
        /// 消费高级点
        /// </summary>
        [String(Name = "eConsumeAdvPoint")]
        public int ConsumeAdvPoint { set; get; }

        /// <summary>
        /// 消费日期
        /// </summary>
        [String(Name = "eConsumeTime")]
        public DateTime ConsumeTime { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [String(Name = "eUserDownIP")]
        public string UserDownIP { get; set; }

        /// <summary>
        /// 资料上传用户名
        /// </summary>
        [String(Name = "eEditor")]
        public string Editor { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [String(Name = "eCensor")]
        public string Censor { get; set; }

        /// <summary>
        /// 是否精品（0=否；1=是）
        /// </summary>
        [String(Name = "eIsBoutique")]
        public int IsBoutique { get; set; }

        /// <summary>
        /// 资料类别ID
        /// </summary>
        [String(Name = "eSoftTypeID")]
        public int SoftTypeID { get; set; }

        /// <summary>
        /// 请求来源 （1=学科网，2=M站）
        /// </summary>
        [String(Name = "eRequestSource")]
        public int RequestSource { get; set; }

        /// <summary>
        /// 请求平台类型 （1=PC，2=ANDROID，3=IOS）
        /// </summary>
        [String(Name = "ePlatForm")]
        public int PlatForm { get; set; }

        /// <summary>
        /// 消费类型（1=储值；2=高级点；3=普通点；4=免费）
        /// </summary>
        [String(Name = "eConsumeType")]
        public int ConsumeType { get; set; }

        /// <summary>
        /// IP下载区域
        /// </summary>
        [String(Name = "eUserDownIPArea",Index =FieldIndexOption.NotAnalyzed)]
        public string IPArea { get; set; }

        /// <summary>
        /// 下载接口（1=网校通，2=网学通，3=视频通，4=普通计点，5=普通计天，6=绑定IP高端网校通）
        /// </summary>
        [String(Name = "eDownInterface")]
        public int DownInterface { get; set; }

        #endregion
    }
}
