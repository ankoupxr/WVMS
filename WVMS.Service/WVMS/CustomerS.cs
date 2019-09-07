using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Core.DB;
using WVMS.IService.WVMS;
using WVMS.Model;
using WVMS.Repository;

namespace WVMS.Service.WVMS
{
    public class CustomerS : BaseRepository<Customer>,ICustomer
    {
        public CustomerS(MyDbContext dbcontext) : base(dbcontext)
        {

        }

    }
}
