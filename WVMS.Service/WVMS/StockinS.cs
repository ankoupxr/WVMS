using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using WVMS.Core.DB;
using WVMS.IService.WVMS;
using WVMS.Model;
using WVMS.Repository;

namespace WVMS.Service.WVMS
{
    public class StockinS : BaseRepository<Stockin>,IStockin
    {
        public StockinS(MyDbContext dbcontext) : base(dbcontext)
        {

        }
        #region 根据ID获取指定条数
        /// <summary>
        /// 根据ID获取指定条数
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="top"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public List<Stockin> GetStockinList(long stockId, int top = 0, bool isDesc = true)
        {
            string order = isDesc ? "StokinId desc" : "ContentId asc";
            return GetList(top, $"StokinId = {stockId}", order).ToList();
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <param name="ordering"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override IQueryable<Stockin> GetPagedList(int pageIndex,
    int pageSize, string predicate, string ordering, params object[] args)
        {
            var result = from a in _dbContext.Stockins
                         select new
                         Stockin()
                         {
                             StockInId = a.StockInId,
                             StockInNo = a.StockInNo,
                             SupplierId = a.SupplierId,
                             StockInType = a.StockInType,
                             StockInStatus = a.StockInStatus,
                             OrderNo = a.OrderNo,
                             CreateBy = a.CreateBy,
                             CreateDate = a.CreateDate,
                             IsDel = a.IsDel,
                             ModifiedBy = a.ModifiedBy,
                             ModifiedDate = a.ModifiedDate,
                             Remark = a.Remark

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
