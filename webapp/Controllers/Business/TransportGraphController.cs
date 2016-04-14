using SmartAdminMvc.ArchipelagoModel.ViewModels;
using SmartAdminMvc.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class TransportGraphController : Controller
    {
        TransportContext db = new TransportContext();
        
        public ActionResult TransGraphIndex()
        {
            return View();
        }

        public ActionResult TransGraphJS()
        {
            var projectView = db.TransProjects.ToList();
            return View(projectView);
        }

        public ActionResult GraphJSView(int id)
        {
            var viewModel = new TransportViewModel();
            viewModel.TransProjectView = db.TransProjects.Where(i => i.TransprojectID == id).Single();
            viewModel.CitieView = db.Cities.Where(i => i.TransProjectID == id).ToList();
            viewModel.StateView = db.States.Where(i => i.TransProjectID == id).ToList();
            return PartialView(viewModel);
        }

        public ActionResult TransGraphFusion()
        {
            var projectView = db.TransProjects.ToList();
            return View(projectView);
        }

        public ActionResult GraphFusionView(int id)
        {
            var viewModel = new TransportViewModel();
            viewModel.TransProjectView = db.TransProjects.Where(i => i.TransprojectID == id).Single();
            viewModel.CitieView = db.Cities.Where(i => i.TransProjectID == id).ToList();
            viewModel.StateView = db.States.Where(i => i.TransProjectID == id).ToList();
            return PartialView(viewModel);
        }
        public ActionResult FusionTest()
        {
            return View();
        }
    }
}