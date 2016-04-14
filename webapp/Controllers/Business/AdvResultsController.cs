//using SmartAdminMvc.DAL;
//using SmartAdminMvc.ArchipelagoModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace SmartAdminMvc.Controllers.Business
//{
//    public class AdvResultsController : Controller
//    {
//        //Initialise db
//        OptimiseContext db = new OptimiseContext();

//        public ActionResult AdvResultsIndex()
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

//        //Ensure that javascript is not cached
//        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
//        //public ActionResult DynamicJava()
//        //{
//        //    //Projects Table
//        //    string jscriptView = '<' + "script type = " + '"' + "text/javascript" + '"' + '>';

//        //    jscriptView += '$' + '(' + '#' + "dt_projects)" + ".dataTable(" + '{';
//        //    jscriptView += '"' + "sDom" + '"' + ':' + '"' + '<' + "'" + "dt-toolbar" + "'" + "<" + "'" + "col-xs-12 col-sm-6" + "'" + "f><" + "'" + "col-sm-6 col-xs-12 hidden-xs" + "'" + "l>r>" + '"' + "+";
//        //    jscriptView += '"' + 't' + '"' + '+';
//        //    jscriptView += '"' + "<'" + "dt-toolbar-footer'" + "<'" + "col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>" + '"' + ',';
//        //    jscriptView += '"' + "autoWidth" + '"' + ": true,";
//        //    jscriptView +=         "preDrawCallback": function() {
//                //    // Initialize the responsive datatables helper once for this table.
//                //    if (!responsiveHelper_project)
//                //    {
//                //        responsiveHelper_project = new ResponsiveDatatablesHelper($('#dt_projects'), breakpointDefinition);
//                //    }
//                //},
//                //    "rowCallback": function(nRow) {
//                //    responsiveHelper_project.createExpandIcon(nRow);
//                //},
//                //    "drawCallback": function(oSettings) {
//                //    responsiveHelper_project.respond();
//                //}
//                //</ script >
//            //    return JavaScript();
//            //});
//        //}
//    }
//}