﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WVMS.Model
{
    ///<summary>
    ///
    ///</summary>
    public partial class Stockin
    {
        public Stockin()
        {
            IsDel = Convert.ToByte("1");
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>
        [Key]
        public long StockInId { get; set; }

        /// <summary>
        /// Desc:入库单号
        /// Default:
        /// Nullable:True
        /// </summary>
        public string StockInNo { get; set; }

        /// <summary>
        /// Desc:入库类型
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? StockInType { get; set; }

        /// <summary>
        /// Desc:供应商
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// Desc:订单号
        /// Default:
        /// Nullable:True
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public int? StockInStatus { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Desc:1 0
        /// Default:1
        /// Nullable:True
        /// </summary>
        public byte? IsDel { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? CreateBy { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Desc:修改人
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}