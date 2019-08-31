using System;
using System.Linq;
using System.Text;
using System.Threading;
using Castle.DynamicProxy;
using NetCore.Common;

namespace NetCore.BasicsFrame
{
    public class LogAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            StringBuilder str = new StringBuilder();
            int State = 0;

            str.Append($"【执行时间】:{DateTime.Now.ToString()} \r\n" + $"【执行的方法】:{invocation.TargetType.Name + "/" + invocation.Method.Name} \r\n"
                + $"【参数】:{string.Join(",", invocation.Arguments)} \r\n");

            try
            {
                invocation.Proceed();  //拦截器执行完成后继续执行该方法
            }
            catch (Exception e)
            {
                State = 1;
                str.Append($"[方法执行中出现异常]:{e.Message + e.InnerException} \r\n");
            }
            str.Append($"【返回结果】:{invocation.ReturnValue} \r\n");

            new LogHelper().ActionInfo(str.ToString(), State);
        }
    }
}
