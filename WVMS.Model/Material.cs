using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WVMS.Model
{
    ///<summary>
    ///
    ///</summary>
    public partial class Material
    {
        public Material()
        {
            IsDel = Convert.ToByte("1");
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [Key]
        public long MaterialId { get; set; }

        /// <summary>
        /// Desc:产品编号
        /// Default:
        /// Nullable:False
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// Desc:产品名称
        /// Default:
        /// Nullable:False
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// Desc:产品类型
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? MaterialType { get; set; }

        /// <summary>
        /// Desc:单位
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? Unit { get; set; }

        /// <summary>
        /// Desc:默认所属货架
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? StoragerackId { get; set; }

        /// <summary>
        /// Desc:默认所属库区
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? ReservoirAreaId { get; set; }

        /// <summary>
        /// Desc:默认所属仓库
        /// Default:
        /// Nullable:True
        /// </summary>
        public long? WarehouseId { get; set; }

        /// <summary>
        /// Desc:安全库存
        /// Default:
        /// Nullable:True
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// Desc:有效期
        /// Default:
        /// Nullable:True
        /// </summary>
        public decimal? ExpiryDate { get; set; }

        /// <summary>
        /// Desc:1 0
        /// Default:1
        /// Nullable:True
        /// </summary>
        public byte? IsDel { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Remark { get; set; }

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