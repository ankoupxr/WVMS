using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Repository;
using WVMS.Model;
using WVMS.Core.DB;
using WVMS.IService.WVMS;

namespace WVMS.Service.WVMS
{
    public class ReservoirareaS : BaseRepository<Reservoirarea>,IReservoirarea
    {
        public ReservoirareaS(MyDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
