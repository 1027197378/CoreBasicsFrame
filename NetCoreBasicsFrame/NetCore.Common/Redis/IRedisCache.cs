using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common
{
    public interface IRedisCache
    {
        //获取 Reids 缓存值
        string GetValue(string key);

        //根据Key获取值并序列化
        TEntity Get<TEntity>(string key);

        //写入缓存
        bool Set(string key, object value);

        //写入缓存
        bool Set(string key, object value, TimeSpan cacheTime);

        //根据Key判断是否存在
        bool Exist(string key);

        //根据Key移除缓存
        void Remove(string key);

        //清除所以缓存
        void Clear();

    }
}
