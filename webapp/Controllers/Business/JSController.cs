using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers.Business
{
    [Authorize]
    public class JSController : Controller
    {
        
        public ActionResult JScript()
        {
            return View();
        }
        public ActionResult JScriptBackGround()
        {
            return View();
        }
    }
}