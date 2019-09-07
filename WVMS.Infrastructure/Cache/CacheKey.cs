using System;
using System.Collections.Generic;
using System.Text;

namespace WVMS.Infrastructure
{
    /// <summary>
    /// 缓存Key
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// 获得省份
        /// </summary>
        public const string NEWREGIONSLIST = "NewRegionsList";
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public const string USERPERMISSIONSLIST = "UserPermissionsList";  

        /// <summary>
        /// 地区
        /// </summary>
        public const string REGIONSLISTALL = "RegionsListAll";

        /// <summary>
        /// 系统配置
        /// </summary>
        public const string CONFIGSYSTEMLISTALL = "ConfigSystemListAll";
    }
}
