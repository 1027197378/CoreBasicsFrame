using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.Interfaces;
using NetCore.Model;
using Swashbuckle.AspNetCore.Swagger;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using NetCore.BasicsFrame;
using System.Reflection;
using NetCore.Services;

namespace NetCoreBasicsFrame
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger配置及授权请求
            services.AddSwaggerGen(s =>
            {
                #region Swagger
                s.SwaggerDoc("v1.0", new Info
                {
                    Version = "v1.0",
                    Title = "Core.API",
                    Description = "API说明文档",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Net.Core.API", Url = "https://github.com/1027197378/CoreBasicsFrame", Email = "1027197378@gmail.com" }
                });
                var xmlPath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "NetCoreBasicsFrame.xml");
                s.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, "NetCore.Model.xml");//这个就是Model层的xml文件名

                s.IncludeXmlComments(xmlModelPath);
                #endregion

                #region Token绑定到ConfigureServices
                var IssuerName = (Configuration.GetSection("Audience"))["Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                s.AddSecurityRequirement(security);

                //方案名称“Basic.Core”可自定义，上下一致即可
                s.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权 直接在框中输入Bearer {token}",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });

            #endregion

            #region 配置Jwt认证服务

            #region 多个角色授权
            // [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            });
            #endregion

            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            // 令牌验证参数
            //2.1【认证】
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,
                     ValidateIssuer = true,
                     ValidIssuer = audienceConfig["Issuer"],//发行人
                     ValidateAudience = true,
                     ValidAudience = audienceConfig["Audience"],//订阅人
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     RequireExpirationTime = true,
                 };
             });
            #endregion

            services.AddDbContext<NetCoreBaseDBContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("NetCore.BasicsFrame")));

            services.AddCors();

            #region 依赖注入(AutoFac)
            return AutofacConfig(services);
            #endregion

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1.0/swagger.json", "ApiHelp-V1.0");
                s.RoutePrefix = "";
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可WWWWWW
            });
            #endregion

            //配置Cors跨域
            app.UseCors(o => o.WithOrigins("127.0.0.1:8001").AllowAnyHeader().AllowAnyMethod());

            //返回错误码
            app.UseStatusCodePages();

            //开启权限认证
            app.UseAuthentication();

            app.UseMvc();
        }

        public IServiceProvider AutofacConfig(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<SYSUserService>().As<ISYSUser>();//以下RegisterAssemblyTypes

            builder.RegisterType<LogAop>();//可以直接替换其他拦截器！一定要把拦截器进行注册

            builder.RegisterAssemblyTypes(Assembly.Load("NetCore.Services"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                      .InterceptedBy(typeof(LogAop));//可以直接替换拦截器

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

    }
}
