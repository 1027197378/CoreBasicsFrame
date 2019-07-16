using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Model
{
    public class MessageModel<T>
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = false;

        /// <summary>
        /// 返回的信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T data { get; set; }
    }
}
