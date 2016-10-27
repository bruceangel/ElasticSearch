using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Nest;

namespace ZXXK_Index.Model
{
    /// <summary>
    /// 资料表实体类
    /// </summary>
    [Serializable]
    [ElasticsearchType(IdProperty = "SoftID")]
    public class Soft
    {
        #region Model        
        /// <summary>
        /// 资料ID
        /// </summary>		
        [Number(NumberType.Long)]
        public int SoftID { get; set; }

        /// <summary>
        /// InfoGuid
        /// </summary>		
        public Guid InfoGuid { get; set; }

        /// <summary>
        /// 资料名称
        /// </summary>		
        public string SoftName { get; set; }

        /// <summary>
        /// 征集ID
        /// </summary>		
        public int CollectID { get; set; }

        /// <summary>
        /// 征集类型(1-普通征集 2-大考征集)
        /// </summary>
        public int CollectType { get; set; }

        /// <summary>
        /// 学科ID
        /// </summary>		
        public int ChannelID { get; set; }

        /// <summary>
        /// 教材ID
        /// </summary>		
        public int ClassID { get; set; }

        /// <summary>
        /// 章ID
        /// </summary>		
        public int ChapterID { get; set; }

        /// <summary>
        /// 节ID
        /// </summary>		
        public int NodeID { get; set; }

        /// <summary>
        /// 套卷ID
        /// </summary>		
        public int WholeID { get; set; }

        /// <summary>
        /// 专题ID
        /// </summary>		
        public int SpecialID { get; set; }

        /// <summary>
        /// 专辑ID
        /// </summary>		
        public int FeatureID { get; set; }

        /// <summary>
        /// 学部ID(0=所有,1=小学,2=初中,3=高中,4=大学)
        /// </summary>		
        public int DepartmentID { get; set; }

        /// <summary>
        /// 年级ID
        /// </summary>		
        public int GradeID { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>		
        public string Prefixion { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>		
        public string FontColor { get; set; }

        /// <summary>
        /// 标题格式
        /// </summary>		
        public int FontType { get; set; }

        /// <summary>
        /// 资料版本ID
        /// </summary>		
        public int VersionID { get; set; }

        /// <summary>
        /// 资料版本名称
        /// </summary>		
        public string SoftVersion { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>		
        public string Keyword { get; set; }

        /// <summary>
        /// 地区ID
        /// </summary>		
        public int AreaID { get; set; }

        /// <summary>
        /// 学校
        /// </summary>		
        public string Author { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>		
        public int AuthorID { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>		
        public int SoftTypeID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>		
        public string SoftType { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>		
        public int SoftCateID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>		
        public string SoftLanguage { get; set; }

        /// <summary>
        /// 适用年份
        /// </summary>		
        public string CopyrightType { get; set; }

        /// <summary>
        /// 资料大小
        /// </summary>		
        public int SoftSize { get; set; }

        /// <summary>
        /// 下载权限
        /// </summary>		
        public string SoftLevel { get; set; }

        /// <summary>
        /// 资料点数
        /// </summary>		
        public decimal SoftPoint { get; set; }

        /// <summary>
        /// 资料储值
        /// </summary>		
        public decimal SoftMoney { get; set; }

        /// <summary>
        /// 等级
        /// </summary>		
        public int Stars { get; set; }

        /// <summary>
        /// 总下载
        /// </summary>		
        public int Hits { get; set; }

        /// <summary>
        /// 本月下载
        /// </summary>		
        public int MonthHits { get; set; }

        /// <summary>
        /// 本周下载
        /// </summary>		
        public int WeekHits { get; set; }

        /// <summary>
        /// 本日下载
        /// </summary>		
        public int DayHits { get; set; }

        /// <summary>
        /// 固顶
        /// </summary>		
        public bool OnTop { get; set; }

        /// <summary>
        /// 推荐
        /// </summary>		
        public bool Elite { get; set; }

        /// <summary>
        /// 高级（0=否；1=是）
        /// </summary>		
        public int IsAward { get; set; }

        /// <summary>
        /// 显示网校通（0=否；1=是）
        /// </summary>		
        public int IsSchool { get; set; }

        /// <summary>
        /// 是否特供（0=否；1=是）
        /// </summary>		
        public int IsSupply { get; set; }

        /// <summary>
        /// 是否精品（0=否；1=是）
        /// </summary>		
        public int IsBoutique { get; set; }

        /// <summary>
        /// 是否解析（0=无；1=有解析）
        /// </summary>		
        public int IsAnalyze { get; set; }

        /// <summary>
        /// 包含缩略图（0=无；1=有）
        /// </summary>		
        public int IncludePic { get; set; }

        /// <summary>
        /// 缩略图URL
        /// </summary>		
        public string PicUrl { get; set; }

        /// <summary>
        /// 简介
        /// </summary>		
        public string Intro { get; set; }

        /// <summary>
        /// 资料地址
        /// </summary>		
        public string FileAddress { get; set; }

        /// <summary>
        /// 返回点数
        /// </summary>		
        public int BackPoint { get; set; }

        /// <summary>
        /// 返回点数比率
        /// </summary>		
        public int BackPointRate { get; set; }

        /// <summary>
        /// 返回储值
        /// </summary>		
        public int BackMoney { get; set; }

        /// <summary>
        /// 返回储值比率
        /// </summary>		
        public int BackMoneyRate { get; set; }

        /// <summary>
        /// 上传用户ID
        /// </summary>		
        [Number(NumberType.Long)]
        public int UserID { get; set; }

        /// <summary>
        /// 上传用户名称
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 审核站长
        /// </summary>		
        public string Censor { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>		
        public DateTime CensorTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>		
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最后点击时间
        /// </summary>		
        public DateTime LastHitTime { get; set; }

        /// <summary>
        /// 好评数
        /// </summary>		
        public int GoodCount { get; set; }

        /// <summary>
        /// 差评数
        /// </summary>		
        public int PoorCount { get; set; }

        /// <summary>
        /// 来源（1=学科网；2=书城；3=大兴）
        /// </summary>		
        public int SourceID { get; set; }

        /// <summary>
        /// 审核(0=待审核；1=审核通过)
        /// </summary>		
        public bool Passed { get; set; }

        /// <summary>
        /// 审核状态（2=待初审；3=待复审；4=已审核；7=待仲裁；6=书城已审核；8=大兴已审核）[回收站：1=退稿；5=永久退稿]
        /// </summary>		
        public int AppState { get; set; }

        /// <summary>
        /// 视频是否自制（0：非学科网自制；1:学科网自制）
        /// </summary>
        public int IsSelfCommand { get; set; }

        /// <summary>
        /// 服务类型（1=服务推荐；2=服务定制）
        /// </summary>
        public int ServiceSource { get; set; }

        /// <summary>
        /// 资料格式（1=图片版；2=文档版）
        /// </summary>
        public int SoftFormat { get; set; }

        /// <summary>
        /// 是否有答案（0:无答案；1:有答案）
        /// </summary>
        public int IsAnswer { get; set; }

        #endregion Model        

        #region ES属性标签规则
        ////属性类型
        //[Number(NumberType.Long)]
        ////存储别名
        //[String(Name = "Age")]
        ////日期格式
        //[Date(Format = "ddmmyyyy")]
        ////如果string 类型的字段不需要被分析器拆分，要作为一个正体进行查询，
        ////需标记此声明，否则索引的值将被分析器拆分。
        //[String(Index = FieldIndexOption.NotAnalyzed)]
        ///// 如需使用坐标点类型需添加坐标点特性，在maping时会自动映射类型。
        //[GeoPoint(Name = "ZuoBiao", LatLon = true)]
        #endregion
    }
}
