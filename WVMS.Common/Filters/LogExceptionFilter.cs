using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Common.Extentions;
using WVMS.Infrastructure.Extentions;
using WVMS.IService.Message;
using WVMS.IService.WVMS;

namespace WVMS.Common.Filters
{
    /// <summary>
    /// 过滤器，记录系统异常错误
    /// </summary>
    public class LogExceptionFilter : IExceptionFilter, IFilterMetadata
    {
        /// <summary>
        /// 
        /// </summary>
        public ILog _mslog;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogExceptionFilter(ILog msLog)
        {
            _mslog = msLog;
        }

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            string msg = context.Exception.Message;
            //错误信息记录数据库
            _mslog.Add(new Model.Message.Log
            {
                IpAddress = context.HttpContext.Connection.LocalIpAddress.ToString(),
                Title = context.Exception.Message,
                Type = 1,
                Url = context.HttpContext.Request.GetAbsoluteUri(),
                UserId = context.HttpContext.User.Identity.GetUserId().ToString(),
                Detail = context.Exception.ToString(),
                UserName = "",
                CreateDate = DateTime.Now
            });
        }
    }
}
