//using SmartAdminMvc.DAL;
//using SmartAdminMvc.ArchipelagoModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace SmartAdminMvc.Controllers.Business
//{
//    public class ResultsController : Controller
//    {
//        //Initialise db
//        OptimiseContext db = new OptimiseContext();
        
//        public ActionResult ResultsIndex()
//        {
//            var projectsView = db.Projects.ToList();
//            return View(projectsView);
//        }

//        //public ActionResult OptimisedVessels(int id)
//        //{
//        //    var optimisedView = db.Projects.Find(id);
//        //    return View(optimisedView);
//        //}

//        public ActionResult AjaxVessels(int id)
//        {
//            var workingProject = db.Projects.Find(id);
//            return PartialView(workingProject);
//        }
        
//    }
//}