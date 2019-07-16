using Microsoft.IdentityModel.Tokens;
using NetCore.Common.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBasicsFrame.AuthHelper.OverWrite
{
    public class JwtHelper
    {

        #region 生成JwtToken字符串
        /// <summary>
        /// 生成JwtToken字符串
        /// </summary>
        /// <param name="JwtModel"></param>
        /// <returns></returns>
        public static string GetJwtToken(TokenModelJwt JwtModel)
        {
            string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
            string aud = Appsettings.app(new string[] { "Audience", "Audience" });
            string secret = Appsettings.app(new string[] { "Audience", "Secret" });

            var claims = new List<Claim>
                {
                 /* 特别重要：
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                 */
                new Claim(JwtRegisteredClaimNames.Jti, JwtModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),
                
                //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
               };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(JwtModel.Role.Split(',').Select(a => new Claim(ClaimTypes.Role, a)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(issuer: iss, claims: claims, signingCredentials: creds);

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            string JwtStr = jwtTokenHandler.WriteToken(jwt);

            return JwtStr;
        }
        #endregion

        #region 解密JwtToken字符串
        /// <summary>
        /// 解密JwtToken字符串
        /// </summary>
        /// <param name="JwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt SerializableJwt(string JwtStr)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtTokenHandler.ReadJwtToken(JwtStr);
            object Role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out Role);
            }
            catch (Exception myEx)
            {
                throw myEx;
            }
            var model = new TokenModelJwt
            {
                Uid = (jwtToken.Id).ObjToInt(),
                Role = Role != null ? Role.ToString() : ""
            };
            return model;
        } 
        #endregion
    }

    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
