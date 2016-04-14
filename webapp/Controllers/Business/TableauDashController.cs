using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers.Business
{
    [Authorize]
    public class TableauDashController : Controller
    {
        
        public ActionResult TableauIndex()
        {
            return View();
        }

        public ActionResult TableauQuadrant()
        {
            return View();
        }

        public ActionResult TableauBank()
        {
            return View();
        }

        public ActionResult TableauCoal()
        {
            return View();
        }
    }
}