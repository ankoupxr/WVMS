using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WVMS.IService.Message;

namespace WVMS.Common
{
    public static class BaseConfig
    {
        /// <summary>
        /// 设置/获取系统的服务提供器
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取配置
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 获取应用程序路径
        /// </summary>
        public static string ContentRootPath { get; set; }

        /// <summary>
        /// 获取静态资源根路径
        /// </summary>
        public static string WebRootPath { get; set; }

        /// <summary>
        /// 设置基础信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="contentRootPath"></param>
        /// <param name="webRootPath"></param>
        public static void SetBaseConfig(IConfiguration config, string contentRootPath, string webRootPath)
        {
            Configuration = config;
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
        }

        /// <summary>
        /// 通过key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            IConfigsys configsys = ServiceProvider.GetService<IConfigsys>();
            return configsys.GetValue(key);
        }
    }
}
