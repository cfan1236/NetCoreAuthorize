using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreAuthorize.Models;
using System.Threading.Tasks;

namespace NetCoreAuthorize.Common
{
    public class BaseController : Controller
    {
        public UserInfo AccountInfo { get; set; }
        public override Task OnActionExecutionAsync(ActionExecutingContext controllerContext, ActionExecutionDelegate cancellationToken)
        {
            //获取用户登录信息
            var authorization = controllerContext.HttpContext.Request.Headers["Authorization"].ToString();
            if (authorization != null && authorization.Contains("BasicAuth"))
            {
                var encrypt = authorization.Split(" ")[1];
                var userInfo = TicketEncryption.VerifyTicket(encrypt, out string client);
                AccountInfo = userInfo;
            }
            return base.OnActionExecutionAsync(controllerContext, cancellationToken);
        }
    }
}
