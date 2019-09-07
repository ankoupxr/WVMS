using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Repository;
using WVMS.Model;
using WVMS.IService.WVMS;
using WVMS.Core.DB;
using System.Linq;
using WVMS;
using WVMS.Model.ViewModel;
using System.Linq.Dynamic.Core;
using WVMS.Model.DTO;

namespace WVMS.Service.WVMS
{
    public class WarehouseS : BaseRepository<Warehouse>,IWarehouse
    {
        public WarehouseS(MyDbContext dbContext):base(dbContext)
        {

        }
        #region 增添货仓
        /// <summary>
        /// 增添仓库
        /// </summary>
        /// <param name="warehouseNo"></param>
        /// <param name="warehouseName"></param>
        /// <param name="remark"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        public long Add(string warehouseNo,string warehouseName,string remark,long createBy)
        {
            var tran = _dbContext.Database.BeginTransaction();
            try
            {
                //仓库表
                var warehouseModel = new Warehouse()
                {
                    WarehouseNo = warehouseNo,
                    WarehouseName = warehouseName,
                    Remark = remark,
                    CreateBy = createBy,
                    CreateDate = DateTime.Now
                };
                _dbContext.Warehouses.Add(warehouseModel);
                _dbContext.SaveChanges();
                tran.Commit();
                return warehouseModel.WarehouseId;
            }
            catch(Exception ex)
            {
                tran.Rollback();
                return 0;
            }
        }

        #endregion

        #region 获取分页记录
        public int GetCount(string predicate, params object[] args)
        {
            var result = from a in _dbContext.Warehouses
                         select new
                         WarehouseDto()
                         {
                             WarehouseId = a.WarehouseId,
                             WarehouseNo = a.WarehouseNo,
                             WarehouseName = a.WarehouseName,
                             Remark = a.Remark,
                             IsDel = a.IsDel,
                             CreateBy = a.CreateBy,
                             CreateDate = a.CreateDate,
                         };
            if (!string.IsNullOrWhiteSpace(predicate))
                result = result.Where(predicate, args);
            return result.Count();
        } 
        #endregion

        #region 分页查询
        public IQueryable<WarehouseDto> GetWarehouseList(int pageIndex, int pageSize, 
            string predicate, string ordering, params object[] args)
        {
            var result = from a in _dbContext.Warehouses
                         orderby a.WarehouseId
                         select new
                         WarehouseDto()
                         {
                             WarehouseId = a.WarehouseId,
                             WarehouseNo = a.WarehouseNo,
                             WarehouseName = a.WarehouseName,
                             Remark = a.Remark,
                             IsDel = a.IsDel,
                             CreateBy = a.CreateBy,
                             CreateDate = a.CreateDate,
                         };
            if (!string.IsNullOrWhiteSpace(predicate))
                result = result.Where(predicate, args);
            if (!string.IsNullOrWhiteSpace(ordering))
                result = result.OrderBy(ordering);
            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        }
        #endregion

    }
}
