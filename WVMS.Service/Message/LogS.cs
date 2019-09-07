using System;
using System.Collections.Generic;
using System.Text;
using WVMS.Core.DB;
using WVMS.IService.Message;
using WVMS.Model.Message;
using WVMS.Repository;

namespace WVMS.Service.Message
{
    public class LogS : BaseRepository<Log>,ILog
    {
        public LogS(MyDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
