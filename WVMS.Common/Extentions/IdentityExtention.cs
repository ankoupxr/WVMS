using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace WVMS.Common.Extentions
{
    public static class IdentityExtention
    {/// <summary>
     /// 获取登录的用户ID
     /// </summary>
     /// <param name="identity">IIdentity</param>
     /// <returns></returns>
        public static int GetUserId(this IIdentity identity)
        {
            if (identity != null)
            {
                var claim = (identity as ClaimsIdentity).Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid);
                if (claim != null)
                    return int.Parse(claim.Value);
            }
            return 0;
        }

        /// <summary>
        /// 获取登录的用户名
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static string GetUserName(this IIdentity identity)
        {
            if (identity != null)
            {
                var claim = (identity as ClaimsIdentity).Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name);
                if (claim != null)
                    return claim.Value;
            }
            return "";
        }
    }
}
