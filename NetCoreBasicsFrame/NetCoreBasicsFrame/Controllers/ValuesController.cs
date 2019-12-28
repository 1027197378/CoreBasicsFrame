using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore.Common;
using NetCore.Interfaces;
using NetCore.Model;
using NetCore.Model.Models;
using NetCoreBasicsFrame.AuthHelper.OverWrite;

namespace NetCoreBasicsFrame.Controllers
{
    /// <summary>
    /// Vule控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly ISYSUser _IsysUser;
        private readonly IRedisCache _IredisCache;

        public ValuesController(ISYSUser isysUser, IRedisCache redisCache)
        {
            this._IsysUser = isysUser;
            _IredisCache = redisCache;
        }

        /// <summary>
        /// Get方法获取ID
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <returns></returns>
        // GET api/values/5
        [HttpPost]
        public ActionResult Get(string Name)
        {
            List<SYSUser> user = new List<SYSUser>();

            user = _IsysUser.QueryStrAsync(a => a.Name == Name).Result;
            return Json(user);
        }

        /// <summary>
        /// Redis
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet]
        public SYSUser GetRidis(string loginName)
        {
            //RedisCache _redisCache = new RedisCache();

            SYSUser userItem = new SYSUser();
            if (_IredisCache.Exist(loginName))
            {
                userItem = _IredisCache.Get<SYSUser>(loginName);
            }
            else
            {
                Task<List<SYSUser>> paramQueryData = _IsysUser.QueryStrAsync(a => a.LoginName == loginName);
                userItem = paramQueryData.Result.FirstOrDefault();
                _IredisCache.Set(loginName, userItem);
            }
            return userItem;
        }


        [HttpPost]
        [Route("GetJwtStr")]
        public dynamic GetJwtStr(string loginName, string passWord)
        {

            string jwtStr = string.Empty;
            bool status = false;

            if (loginName == "Admin")
            {
                TokenModelJwt tokenModel = new TokenModelJwt()
                {
                    Uid = 1,
                    Role = "Admin",
                    Work = "管理员"
                };
                jwtStr = JwtHelper.GetJwtToken(tokenModel);
                status = true;
            }
            else
            {
                jwtStr = "验证失败！";
            }
            return Ok(new { success = status, data = jwtStr });
        }
    }
}
