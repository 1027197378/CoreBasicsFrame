using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore.Model.Models
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public class SYSUser
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [Column(Order =1,TypeName = "uniqueidentifier")]
        public Guid UserID { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        [Column(TypeName ="NvarChar(20)")]
       public string Name { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        [Column(TypeName ="NvarChar(18)")]
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Column(TypeName = "NvarChar(18)")]
        public string LoginPwd { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Column(TypeName ="Int")]
        public Nullable<int> Age { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Column(TypeName = "NvarChar(15)",Order =5)]
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Column(TypeName = "NvarChar(100)")]
        public string Addrees { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column(TypeName = "uniqueidentifier")]
        public Guid? CreatUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime? CreatTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Column(TypeName = "uniqueidentifier")]
        public Guid? UpdateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime? UpdateTime { get; set; }

    }
}
