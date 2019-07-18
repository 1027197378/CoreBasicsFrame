using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore.Model;
using NetCore.Model.Models;
using NetCore.Repository.BaseRepository;
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
        NetCoreBaseDBContext mycontex;
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
        /// <param name="token">token</param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            //BaseRepository<SYSUser> my = new BaseRepository<SYSUser>();
            SYSUser myUser = new SYSUser();
            Guid myid = new Guid(id);
            myUser = mycontex.Set<SYSUser>().Where(u => u.UserID == myid).FirstOrDefault();

            //Task<SYSUser> myUser = my.QueryById(id);
            return Json(myUser);
            //6F9619FF-8B86-D011-B42D-00C04FC964FF
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
