
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;

namespace NetCoreAuthorize.Common
{
    /// <summary>
    /// 安全认证过滤器
    /// </summary>
    public class AuthorizeFilter : IActionFilter, IAuthorizationFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //允许匿名访问
            if (context.HttpContext.User.Identity.IsAuthenticated ||
                context.Filters.Any(item => item is IAllowAnonymousFilter))
                return;


            var httpContext = context.HttpContext;

            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            var request = context.HttpContext.Request;
            var authorization = request.Headers["Authorization"].ToString();
            if (authorization != null && authorization.Contains("BasicAuth"))
            {
                //当前登录用户ticket
                var current_ticket = authorization.Split(" ")[1];
                var userInfo = TicketEncryption.VerifyTicket(current_ticket, out string dec_client);
                if (userInfo != null)
                {
                    //同一个终端多次登录挤下线功能 返回403
                    if (userInfo.ticket != current_ticket && userInfo.client.ToString() == dec_client)
                    {
                        #region 多设备挤下线代码
                        var response = new HttpResponseMessage();
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new JsonResult("Forbidden:The current authorization has expired");
                        #endregion

                        return;
                    }
                    else
                    {
                        return;
                    }
                }

            }
            // 401 未授权
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new JsonResult("Forbidden:Tiket Invalid");



        }


    }
}