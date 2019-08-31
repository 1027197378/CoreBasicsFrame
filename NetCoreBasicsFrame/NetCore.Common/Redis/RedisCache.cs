using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCore.Common
{
    public class RedisCache : IRedisCache
    {
        private readonly string redisConnenctionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisLock = new object();

        public RedisCache()
        {
            redisConnenctionString = Appsettings.app(new string[] { "RedisCaching", "ConnectionString" });

            if (string.IsNullOrWhiteSpace(redisConnenctionString))
            {
                throw new ArgumentException("AppSettings is Not Config");
            }
            redisConnection = GetRedisConnection();
        }

        /// <summary>
        /// 单例模式获取实例
        /// </summary>
        /// <returns></returns>
        public ConnectionMultiplexer GetRedisConnection()
        {
            if (redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            lock (redisLock)
            {
                if (this.redisConnection != null)
                {
                    redisConnection.Dispose();
                }
                try
                {
                    redisConnection = ConnectionMultiplexer.Connect(redisConnenctionString);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return redisConnection;
        }


        public bool Exist(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public T Get<T>(string key)
        {
            var vaule = redisConnection.GetDatabase().StringGet(key);

            if (vaule.HasValue)
            {
                return SerializeHelper.Deserialize<T>(vaule);
            }
            return default(T);
        }

        public string GetValue(string key)
        {
            return redisConnection.GetDatabase().StringGet(key);
        }

        public bool Set(string key, object value)
        {
            if (value != null)
            {
                return redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value));
            }
            return false;
        }

        public bool Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                return redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
            return false;
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Clear()
        {
            foreach (var end in GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(end);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }
    }
}
