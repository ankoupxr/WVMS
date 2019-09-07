using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WVMS.Model.Message
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    public class Configsys
    {
        /// <summary>
        /// 表ID
        /// </summary>
        [Key]
        public int CsId { get; set; }
        /// <summary>
        /// keyName
        /// </summary>
        [Display(Name = "keyName")]
        [Required(ErrorMessage = "此项不能为空")]
        public string KeyName { get; set; }
        /// <summary>
        /// keyvalue
        /// </summary>
        [Display(Name = "keyvalue")]
        [Required(ErrorMessage = "此项不能为空")]
        public string KeyValue { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Required(ErrorMessage = "此项不能为空")]
        public int KeyType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
