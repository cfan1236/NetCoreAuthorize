using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAuthorize.Utils
{
    public class Randoms
    {
        /// <summary>
        /// 随机获取N个字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetRandomKey(int num)
        {
            string words = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            return GetRandom(num, words);
        }
        /// <summary>
        /// 随机获取N个字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetRandomString(int num)
        {
            string words = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            return GetRandom(num, words);
        }
        /// <summary>
        /// 随机获取N数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetRandomNumber(int num)
        {
            string words = "1234567890";
            return GetRandom(num, words);
        }

        private static string GetRandom(int num, string words)
        {
            var arr = words.ToArray();
            StringBuilder sb = new StringBuilder(num);
            for (int i = 0; i < num; i++)
            {
                var uuid = Guid.NewGuid().ToByteArray();
                int seed = DateTime.Now.Millisecond + uuid.Sum(s => s);
                Random random = new Random(seed + i);
                var word = random.Next(0, arr.Length);
                sb.Append(arr[word]);
            }
            return sb.ToString();
        }
    }
}
