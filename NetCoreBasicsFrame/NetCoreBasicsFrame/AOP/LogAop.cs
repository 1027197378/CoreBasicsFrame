using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
namespace NetCore.BasicsFrame
{
    public class LogAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            string logStr = $"{DateTime.Now.ToString("yyyyMMddHHmmss")} \r\n" +
                $"[执行的方法]:{invocation.Method.Name} \r\n" + $"[参数]:{invocation.Arguments.Select(a => a ?? "").ToString().ToArray()}";
            Console.WriteLine("啊实打实的");
            //拦截器执行完成后继续执行该方法
            invocation.Proceed();
            Console.WriteLine("阿萨大");
            logStr += $"[方法执行完毕返回的结果]:{invocation.ReturnValue}";

            //输出到Log文件中
            string path = Directory.GetCurrentDirectory() + @"\Log";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + $@"\SystemLog-{DateTime.Now.ToString("yyyyMMdd")}.log";

            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(logStr);
            sw.Close();
        }
    }
}
