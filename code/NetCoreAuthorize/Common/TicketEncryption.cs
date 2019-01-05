using NetCoreAuthorize.Models;
using NetCoreAuthorize.Utils;
using System;

namespace NetCoreAuthorize.Common
{
    public class TicketEncryption
    {
        //加密key 实际中请用配置文件配置
        private static readonly string key = "yvDlky7GXGtlPCGr";
        /// <summary>
        /// 获取一个新的ticket
        /// </summary>
        /// <param name="guid">用户的guid</param>
        /// <param name="client">客户端</param>
        /// <returns></returns>
        public static string GenerateTicket(string guid, string client)
        {
            //随机key
            string randomKey = Randoms.GetRandomString(15);
            var keys = key + randomKey;
            var desStr = Encryption.DesEncrypt(guid + "&" + client, keys);
            var base64Str = Encryption.Base64Encrypt(desStr) + randomKey;
            return base64Str;
        }

        /// <summary>
        /// 校验ticket
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static UserInfo VerifyTicket(string encryptStr,out string client)
        {
            try
            {
                RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
                //加密原型:guid&client; 如:08e80f78-95ad-427c-b506-a5f1504e29ac&ios
                string randomKey = encryptStr.Substring(encryptStr.Length - 15);
                var base64 = encryptStr.Substring(0, encryptStr.Length - 15);
                var deBase64 = Encryption.Base64Decrypt(base64);
                var keys = key + randomKey;
                string ticketInfo = Encryption.DesDecrypt(deBase64, keys);
                var guid = ticketInfo.Split("&")[0];
                client = ticketInfo.Split("&")[1];
                string redisKey = "ticket_" + guid;
                var obj = redisHelper.Get<UserInfo>(redisKey);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
