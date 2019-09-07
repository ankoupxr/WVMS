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
    public class WarehouseController : BaseController
    {
        private readonly IWarehouse _warehouseS;
        public WarehouseController(IWarehouse warehouseS)
        {
            _warehouseS = warehouseS;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public IActionResult List(int page,int limit,string keyWords)
        {
            StringBuilder StrWhere = new StringBuilder("1=1");
            if(!string.IsNullOrWhiteSpace(keyWords))
            {
                StrWhere.Append("AND (WarehouseName.Contain(@0))");
            }
            var list = _warehouseS.GetWarehouseList(page, limit, StrWhere.ToString(), "WarehouseId desc", keyWords);
            int recordCount = _warehouseS.GetRecordCount(StrWhere.ToString(), keyWords);
            return Json(ListResult(list, recordCount));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 添加保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Warehouse model)
        {
            bool a = _warehouseS.Add(model);
            return Json(AjaxResult(a));
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var model = _warehouseS.GetModel(c => c.WarehouseId == id);
            if(model == null)
            {
                return Content("查找不到数据！");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Warehouse warehouse)
        {
            var model = _warehouseS.GetModel(c => c.WarehouseId == warehouse.WarehouseId);
            if(model == null)
            {
                return Json(Failed("查询不到数据"));
            }
            bool b = _warehouseS.Update(model);
            return Json(AjaxResult(b));
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteAll(List<long> ids)
        {
            var list = _warehouseS.GetList(c => ids.Contains(c.WarehouseId));
            bool b = _warehouseS.Delete(list);
            return Json(AjaxResult(b));
        }
    }
}