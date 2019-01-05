namespace NetCoreAuthorize.Models
{
    public class loginParam
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 登录终端 (可以实现同一个终端挤下线功能 )
        /// </summary>
        public Enums.ClientType client { get; set; }
    }
}
