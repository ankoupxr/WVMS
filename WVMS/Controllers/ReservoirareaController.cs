using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WVMS.Common.Controllers;

namespace WVMS.Controllers
{
    public class ReservoirareaController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}