using System.ComponentModel;

namespace elephantshine.Models.Enum
{
    public enum EnumCategory
    {
        /// <summary>
        /// LOGO設計
        /// </summary>
        [DisplayName("LOGO設計")]
        Logo,
        /// <summary>
        /// 形象設計
        /// </summary>
        [DisplayName("形象設計")]
        Image,
        /// <summary>
        /// 包裝設計
        /// </summary>
        [DisplayName("包裝設計")]
        Packing,
        /// <summary>
        /// 活動文宣
        /// </summary>
        [DisplayName("活動文宣")]
        Activity,
        /// <summary>
        /// 書籍型錄
        /// </summary>
        [DisplayName("書籍型錄")]
        Book,
        /// <summary>
        /// 教材編排
        /// </summary>
        [DisplayName("教材編排")]
        Study,
        /// <summary>
        /// 婚卡喜帖
        /// </summary>
        [DisplayName("婚卡喜帖")]
        Wedding,
        /// <summary>
        /// 印刷輸出
        /// </summary>
        [DisplayName("印刷輸出")]
        Print
    }
}