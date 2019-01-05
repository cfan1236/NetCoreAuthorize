using NetCoreAuthorize.Models.Enums;

namespace NetCoreAuthorize.Models
{
    public class UserInfo
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string useName { get; set; }
        public string trueName { get; set; }
        /// <summary>
        /// 登录成功后的ticket
        /// </summary>
        public string ticket { get; set; }

        public ClientType client { get; set; }
    }
}
