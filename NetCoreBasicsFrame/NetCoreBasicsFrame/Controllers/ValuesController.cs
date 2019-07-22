using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore.Interfaces;
using NetCore.Model;
using NetCore.Model.Models;
using NetCoreBasicsFrame.AuthHelper.OverWrite;

namespace NetCoreBasicsFrame.Controllers
{
    /// <summary>
    /// Vule控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly ISYSUser _isysUser;

        public ValuesController(ISYSUser isysUser)
        {
            this._isysUser = isysUser;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get方法获取ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">token</param>
        /// <returns></returns>
        // GET api/values/5
        [HttpPost]
        public ActionResult Get(string Ids, string id, string name)
        {
            object[] param = id.Split(',');


            Task<List<SYSUser>> data1 = _isysUser.QueryForSql("SELECT * FROM SYSUser");

            Task<List<SYSUser>> data2 = _isysUser.QueryForSql("SELECT * FROM SYSUser WHere UserID ={0}",param);


            Expression<Func<SYSUser, bool>> where = null;
            Expression<Func<SYSUser, object>> order = null;
            //where = a => a.Name.Contains(name);
            order = a => a.CreatTime;
            Task<List<SYSUser>> wheredata = _isysUser.QueryStrAsync(where);

            Task<List<SYSUser>> orderwheredata = _isysUser.QueryStrAsync(where, order, false);

            Task<SYSUser> user = _isysUser.QueryById(new Guid(id));
            IEnumerable<SYSUser> newUser = _isysUser.LinqQuery(name);
            var data = new
            {
                lambda = user.Result,
                linq = newUser,
                where = wheredata.Result,
                orderwhere = orderwheredata.Result,
                data1 = data1.Result,
                data2 = data2.Result
            };
            MessageModel<object> resule = new MessageModel<object>();
            resule.success = true;
            resule.msg = "Ok";
            resule.data = data;
            return Json(resule);
        }

        [HttpPost]
        [Route("GetJwtStr")]
        public dynamic GetJwtStr(string loginName, string passWord)
        {

            string jwtStr = string.Empty;
            bool status = false;

            if (loginName == "Admin" && passWord == "123")
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
