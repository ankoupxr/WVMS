using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WVMS.Common.Controllers;
using WVMS.IService.WVMS;
using WVMS.Model;

namespace WVMS.Controllers
{
    public class StockinController : BaseController
    {
        IStockin _stockin;
        public StockinController(IStockin stockin)
        {
            _stockin = stockin;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="keyWords"></param>
        /// <param name="sortField"></param>
        /// <param name="sortType"></param>
        /// <param name="status"></param>
        /// <param name="stockinId"></param>
        /// <returns></returns>
        public IActionResult StockinList(int page,int limit,string keyWords,string sortField
            ,string sortType,int status,int stockinId)
        {
            StringBuilder strWhere = new StringBuilder("1=1");
            if(!string.IsNullOrWhiteSpace(keyWords))
            {
                strWhere.Append("AND StockInNo.Contains(@0");
            }
            if(status > 0)
            {
                strWhere.Append("AND StockInState=@1");
            }
            string orderStr = "StockInId Desc";

            if(stockinId > 0)
            {
                strWhere.Append(" AND StockInId=@2  ");
            }

            if (!string.IsNullOrWhiteSpace(sortField) && !string.IsNullOrWhiteSpace(sortType))
            {
                orderStr = sortField + " " + sortType;
            }

            var list = _stockin.GetPagedList(page, limit, strWhere.ToString(), orderStr, keyWords, status, stockinId);
            int recordCount = _stockin.GetRecordCount(strWhere.ToString(), keyWords, status, stockinId);
            return Json(ListResult(list, recordCount));
        }

        /// <summary>
        /// 添加入库单
        /// </summary>
        /// <returns></returns>
        public IActionResult StockInAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StockInAdd(Stockin stockin)
        {
            stockin.CreateDate = DateTime.Now;
            stockin.StockInStatus = 1;
            bool b = _stockin.Add(stockin);
            return Json(AjaxResult(b));
        }

        public IActionResult StockinEdit(int id)
        {
            var model = _stockin.GetModel(c => c.StockInId == id);
            if(model == null)
            {
                return Content("查询不到数据");
            }
            model.StockInNo = _stockin.GetModel(c => c.StockInId == model.StockInId).StockInNo;

            return View(model);
        }

        public IActionResult StockinEdit(Stockin stockin)
        {
            var model = _stockin.GetModel(c => c.StockInId == stockin.StockInId);
            if(model == null)
            {
                return Json(Failed("查询不到数据"));
            }
            model.StockInId = stockin.StockInId;
            model.StockInNo = stockin.StockInNo;
            model.StockInStatus = stockin.StockInStatus;
            model.StockInType = stockin.StockInType;
            model.SupplierId = stockin.SupplierId;
            model.OrderNo = stockin.OrderNo;
            model.Remark = stockin.Remark;
            model.CreateBy = stockin.CreateBy;
            model.CreateDate = stockin.CreateDate;
            model.ModifiedBy = stockin.ModifiedBy;
            model.ModifiedDate = stockin.ModifiedDate;

            bool b = _stockin.Update(model);
            return Json(AjaxResult(b));
        }
    }
}