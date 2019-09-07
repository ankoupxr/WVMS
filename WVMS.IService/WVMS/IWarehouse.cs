using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WVMS.IRepository;
using WVMS.Model;
using WVMS.Model.DTO;

namespace WVMS.IService.WVMS
{
    public interface IWarehouse : IBaseRepository<Warehouse>
    {
        long Add(string warehouseNo, string warehouseName, string remark, long createBy);
        IQueryable<WarehouseDto> GetWarehouseList(int pageIndex, int pageSize,
            string predicate, string ordering, params object[] args);

        int GetCount(string predicate, params object[] args);
    }
}
