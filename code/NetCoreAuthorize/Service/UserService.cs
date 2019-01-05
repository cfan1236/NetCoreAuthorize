using NetCoreAuthorize.Common;
using NetCoreAuthorize.IService;
using NetCoreAuthorize.Models;
using NetCoreAuthorize.Utils;
using System;

namespace NetCoreAuthorize.Service
{
    public class UserService : IUserService
    {
        private RedisHelper _redisHelper;
        public UserService()
        {
            _redisHelper = new RedisHelper("127.0.0.1:6379");
        }
        public UserInfo login(loginParam param)
        {
            if (param.user == "admin" && param.password == "123456")
            {
                //添加测试用户信息
                UserInfo u = new UserInfo()
                {
                    useName = "admin",
                    trueName = "张三",
                    id = 1001,
                    guid = "73e01eab-210d-4d19-a72a-d0d64e053ec0",
                    client = param.client
                };
                //登录成功产生一个ticket
                u.ticket = TicketEncryption.GenerateTicket(u.guid, param.client.ToString());
                string redisKey = "ticket_" + u.guid;
                //保存登录信息到redis
                _redisHelper.Set<UserInfo>(redisKey, u, TimeSpan.FromDays(3));

                return u;
            }
            return null;
        }
    }
}
