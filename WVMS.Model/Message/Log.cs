using System;
using System.Collections.Generic;
using System.Text;

namespace WVMS.Model.Message
{
    /// <summary>
    /// 系统日志记录
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int LogId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 1错误日志 2操作日志
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }
    }
}
