﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WVMS.Model
{
    ///<summary>
    ///
    ///</summary>
    public partial class Inventoryrecord
    {
        public Inventoryrecord()
        {
            this.IsDel = Convert.ToByte("1");
            this.CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [Key]
        public long InventoryrecordId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? StockInDetailId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Desc:1 0
        /// Default:
        /// Nullable:False
        /// </summary>
        public byte IsDel { get; set; }

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