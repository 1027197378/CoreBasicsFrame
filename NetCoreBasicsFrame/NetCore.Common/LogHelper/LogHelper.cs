using Blog.Core.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore.Common
{
    public class LogHelper
    {
        private readonly object logLock = new object();

        public void ActionInfo(string log, int state)
        {
            if (state == 0)
            {
                log += "[ActionState]:OK \r\n";
            }
            else
            {
                log += "[ActionState]:ERROR \r\n";
            }

            WriteLog(log, state);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        private void WriteLog(string log, int state)
        {
            string path = Directory.GetCurrentDirectory() + @"\Log";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + $@"\ActionLog_{DateTime.Now.ToString("yyyyMMdd")}.log";

            Parallel.For(0, 1, e =>
            {
                LogLock.OutSql2Log("ActionLog_"+ DateTime.Now.ToString("yyyyMMdd"), new string[] { log });

                //写入错误日志
                if (state == 1)
                {
                    LogLock.OutSql2Log("ErrorLog_"+ DateTime.Now.ToString("yyyyMMdd"), new string[] { log });
                }
            });
         }
    }
}
