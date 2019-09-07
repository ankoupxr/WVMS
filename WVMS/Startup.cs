using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WVMS.Core;
using WVMS.Core.DB;
using WVMS.Model;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WVMS.Common.Filters;
using WVMS.Common;
using WVMS.IService.WVMS;
using WVMS.Service.WVMS;
using WVMS.Service.Message;
using WVMS.IService.Message;

namespace WVMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;

            BaseConfig.SetBaseConfig(Configuration, env.ContentRootPath, env.WebRootPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region  启用DbContext
            var sqlConnection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContextPool<MyDbContext>(option => option.UseSqlServer(sqlConnection));

            //注入数据
            services.AddScoped<IWarehouse,WarehouseS>();
            services.AddScoped<IConfigsys,ConfigsysS>();
            services.AddScoped<ILog, LogS>();
            services.AddScoped<ICustomer, CustomerS>();
            services.AddScoped<IStockin, StockinS>();
            services.AddScoped<IReservoirarea, ReservoirareaS>();
            #endregion

            #region Identity
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyDbContext>();
            #endregion



            #region MVC
            services.AddMvc(options =>
               {
                   options.Filters.Add<LogExceptionFilter>();//设置全局异常处理过滤器
            }).AddJsonOptions(options =>
            {//json序列化设置
                //json序列化设置默认驼峰命名
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            #endregion

            #region 设置系统的依赖注入的服务提供器
            BaseConfig.ServiceProvider = services.BuildServiceProvider(); 
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseFileServer();

            app.UseAuthentication();

            //跨域请求设置 
            app.UseCors(builder => builder
            //允许任何来源
            .AllowAnyOrigin()
            //所有请求方法
             .AllowAnyMethod()
             //所有请求头
             .AllowAnyHeader());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
