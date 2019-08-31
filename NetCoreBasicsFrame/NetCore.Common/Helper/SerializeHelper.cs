using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common
{
    public class SerializeHelper
    {

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(object value)
        {
            if (value == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

    }
}
