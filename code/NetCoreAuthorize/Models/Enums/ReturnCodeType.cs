using System.ComponentModel;

namespace NetCoreAuthorize.Models.Enums
{
    public enum ReturnCodeType
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("fail")]
        Fail = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("success")]
        Success = 1,
    }
}
