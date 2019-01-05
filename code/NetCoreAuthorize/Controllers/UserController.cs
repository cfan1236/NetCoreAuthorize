using NetCoreAuthorize.Common;
using NetCoreAuthorize.IService;
using NetCoreAuthorize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NetCoreAuthorize.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public MessageInfo<UserInfo> login([FromBody]loginParam param)
        {
            MessageInfo<UserInfo> ret = new MessageInfo<UserInfo>();
            ret.SetStatusCode(Models.Enums.ReturnCodeType.Fail, null);
            if (param != null)
            {
                var user = _userService.login(param);
                if (user != null)
                {
                    ret.SetStatusCode(Models.Enums.ReturnCodeType.Success, user);
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取当前用户名 用于测试ticket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageInfo<string> GetUserName()
        {
            MessageInfo<string> ret = new MessageInfo<string>();
            if (AccountInfo != null)
                ret.SetStatusCode(Models.Enums.ReturnCodeType.Success, AccountInfo.useName);
            return ret;
        }


    }
}
