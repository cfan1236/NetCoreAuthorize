using NetCoreAuthorize.Models;

namespace NetCoreAuthorize.IService
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        UserInfo login(loginParam param);
    }
}
