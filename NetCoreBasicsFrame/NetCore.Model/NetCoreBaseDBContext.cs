using Microsoft.EntityFrameworkCore;
using NetCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Model
{
    public class NetCoreBaseDBContext : DbContext
    {

        public NetCoreBaseDBContext(DbContextOptions<NetCoreBaseDBContext> options) : base(options)
        {

        }

        /// <summary>
        /// 系统用户表
        /// </summary>
        public DbSet<SYSUser> SYSUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
