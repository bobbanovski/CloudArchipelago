using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers.Business
{
    [Authorize]
    public class ContosoUniController : Controller
    {
        // GET: ContosoUni
        public ActionResult ContosoUni()
        {
            return View();
        }
    }
}