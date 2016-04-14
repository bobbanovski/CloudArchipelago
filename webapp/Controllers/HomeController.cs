#region Using

using SmartAdminMvc.ArchipelagoModel;
using SmartAdminMvc.DAL;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //Initialise database
        TransportContext db = new TransportContext();

        //ViewModel for this controller
        //public class HomeViewModel
        //{
        //    public Project selectedProject { get; set; }
        //    public IEnumerable<Project> projectList { get; set; }
        //}

        // GET: home/index
        public ActionResult Index()
        {
            //var projectViewModel = db.Projects.ToList();
            return View();
        }

        //[HttpGet]
        //public ActionResult _AjaxIndex(int id)
        //{
        //    Project viewModel = new Project();
        //        viewModel = db.Projects.Find(id);
        //    ViewBag.Flag = id.ToString();
        //    return PartialView(viewModel);
        //}

        //[HttpGet]
        //public ActionResult _VesselsTable(int id)
        //{
        //    //Project viewModel = new Project();
        //    //    viewModel = db.Projects.Find(id);
        //    var vesselViewModel = db.Projects.Find(id).Vessels.ToList();
        //    return PartialView(vesselViewModel);
        //}
        public ActionResult _ComponentsTable(int id)
        {
            //Project addProject = new Project();
            //    addProject = db.Projects.Find(id);
            //IEnumerable<Project> viewModel = new List<Project>();
            //viewModel = db.Projects.Find(id).Vessels.ToList();
            return PartialView();
        }

        public ActionResult Social()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }
    }
}