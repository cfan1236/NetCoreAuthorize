using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace NetCoreAuthorize.Utils
{
    public class RedisHelper
    {
        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        public RedisHelper(string connection)
        {
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
        }


        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = ConvertJson(obj);
            return db.StringSet(key, json, expiry);
        }
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return ConvertObj<T>(db.StringGet(key));
        }
        private string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }
        private T ConvertObj<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public bool KeyDelete(string key)
        {
            return db.KeyDelete(key);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return db.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return db.StringGet(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }
    }

}
