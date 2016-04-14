//using SmartAdminMvc.ArchipelagoModel;
//using System;
//using System.Collections.Generic;
//using System.Data.OleDb;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
////using Excel = Microsoft.Office.Interop.Excel;
//using GemBox.Spreadsheet;
//using System.Data;
//using System.Data.Entity;
//using System.Diagnostics;
//using System.Globalization;
//using SmartAdminMvc.DAL;
//using SmartAdminMvc.ArchipelagoModel.ViewModels;
//using System.Web.Routing;
//using Excel;

//namespace SmartAdminMvc.Controllers.Business
//{
//    [Authorize]
//    public class NewProjectController : Controller
//    {
//        //Initialise the database for the entire controller
//        OptimiseContext db = new OptimiseContext();

//        public ViewResult NewProjectIndex()
//        {
//            //List all projects, from the database

//            //Send the current data and time to the view
//            DateTime currentTime = DateTime.Now;
//            var culture = new CultureInfo("en-GB");
//            string savedtime = currentTime.ToString(culture);
//            ViewBag.CurrentTime = savedtime;

//            //THIS DOES NOT WORK WITHOUT using Data.Entity
//            var projects = db.Projects.ToList();

//            return View(projects);
//            //Send list of current projects to the view
//            //Specify Eager Loading of database
            
//            //return View(db.Projects.ToList());
//        }

//        public ActionResult ProjectDelete(int id)
//        {
//            Project proj = db.Projects.Find(id);
//            db.Entry(proj).State = System.Data.Entity.EntityState.Deleted;
//            db.SaveChanges();
//            return RedirectToAction("NewProjectIndex");
//        }

//        //This method used post with parameters sent to controllers
//        public ActionResult NewProjectCreate()
//        {
//            if (TempData["Msg"] != null)
//            {
//                string M = TempData["Msg"].ToString();
//                ViewBag.UploadResult = M;
//            }
//            if (TempData["Error"] != null)
//            {
//                string errorMessage = TempData["Error"].ToString();
//                ViewBag.Error = errorMessage;
//            }
//            if (TempData["ListVessels"] != null)
//            {
//                ViewBag.ListVessels = TempData["ListVessels"];
//            }
//            if (TempData["ListComponents"] != null)
//            {
//                ViewBag.ListComponents = TempData["ListComponents"];
//            }

//            //Send current date and time to view
//            DateTime currentTime = DateTime.Now;
//            var culture = new CultureInfo("en-GB");
//            string savedtime = currentTime.ToString(culture);
//            ViewBag.CurrentTime = savedtime;

//            var viewModel = new Project();

//            //create a new instance of a view model to send to the view
//            //if (TempData["ViewModel"] != null)
//            //{
//            //    viewModel.ProjectName = TempData["ViewModel"];
//            //} 
//            return View(viewModel);
//        }
//        [HttpPost]
//        public ActionResult NewProjectCreate(string ProjectName, string ProjectDate)
//        {
//            //Add Project object to database - this doubles as a view model
//            Project p = new Project();
//            p.ProjectName = ProjectName;
//            p.ProjectDate = ProjectDate;
//            db.Entry(p).State = System.Data.Entity.EntityState.Added;
//            db.SaveChanges();

//            //Get the projectID of the newly created project (will be the highest value in the db)
//            int ProjID = db.Projects.Max(projid => projid.ProjectID);
//            TempData["Msg"] = ProjID.ToString();

//            //Get excelfile from the form
//            HttpPostedFileBase excelTemplate = Request.Files["excelfile"];
//            //Use this as a flag
//            //TempData["Msg"] = "details uploaded from form were: " + ProjectName + " and " + ProjectDate + " and " + excelTemplate;
//            //Locate a folder on the server and save excel file there, overriding it if is already there.
//            string path = Server.MapPath("~/App_Data/Project/Storage/" + excelTemplate.FileName);

//            if (System.IO.File.Exists(path))
//                System.IO.File.Delete(path);
//            excelTemplate.SaveAs(path);

//            if (excelTemplate == null || excelTemplate.ContentLength == 0)
//            {
//                TempData["Error"] = "File has no data <br />";
//                return RedirectToAction("NewProjectCreate");
//            }
//            else {
                
//                //Get binary file stream of excelTemplate
//                Stream stream = excelTemplate.InputStream;
//                IExcelDataReader reader = null;

//                if (excelTemplate.FileName.EndsWith(".xls"))
//                {
//                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
//                }
//                else if (excelTemplate.FileName.EndsWith(".xlsx"))
//                {
//                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
//                }
//                else
//                {
//                    ModelState.AddModelError("File", "This file format is not supported");
//                    return View();
//                }


//                //Create vessel List object
//                List<Vessel> vesselList = new List<Vessel>();
                
//                //Get all the data in the Vessels sheet
//                var excelData = new ExcelData(path);
//                var vessels = excelData.getData("Vessel Requirements");
//                foreach (var row in vessels)
//                {
//                    var vessel = new Vessel()
//                    {
//                        Name = row["Vessel Name"].ToString(),
//                        
//                    };
//                    vesselList.Add(vessel);
//                    //Enter the vessel into the database
//                    db.Entry(vessel).State = System.Data.Entity.EntityState.Added;
//                }

//                //Save vessels to Vessels list of project view model
//                p.Vessels = vesselList;
                
//                //Save the list of vessel to the Vessels Table
//                db.SaveChanges();

//                //Get all data in the Components sheet
//                List<Component> componentList = new List<Component>();
                
//                var components = excelData.getData("Avaliable Components");
//                foreach (var row in components)
//                {
//                    var component = new Component()
//                    {
//                        
//                    };
//                    componentList.Add(component);
//                    //save component to database
//                    db.Entry(component).State = System.Data.Entity.EntityState.Added;
//                }

//                //Fill out the components section of the project view model
//                p.Components = componentList;
//                //TempData["ViewModel"] = p;
//                //Save the list of components to the Components Table in the database
//                db.SaveChanges();


//                //Finish getting all the components in the Components sheet

//                //reader.IsFirstRowAsColumnNames = true;

//                DataSet result = reader.AsDataSet();
//                reader.Close();

//                return View(p);
//            }
//        }

//        //Using context object
//        public ActionResult InsertTemplateContext(FormCollection fc)
//        {
//            string projectName = fc["ProjectName"].ToString();
//            string projectDate = fc["ProjectDate"].ToString();
//            string excelTemplate = fc["excelTemplate"].ToString();
            
//            return RedirectToAction("NewProjectCreate");
//        }

//        public ActionResult NewProjectDetails(int id)
//        {
//            Project p = db.Projects.Find(id);
//            return View(p);
//        }

        

//        public ActionResult BasicCharts()
//        {
//            Project selectedProject = db.Projects.Find(15);
//            //ExcelWorksheet vesselsSheet = macrofile.Worksheets["Vessel Requirements!"];
//            string dataPreString = "[";

//            //FusionCharts
//            foreach (var Vessel in selectedProject.Vessels)
//            {
//                dataPreString = dataPreString + '{' + '"' + "label" + '"' + ':' + '"' + Vessel.Name + '"' + ',' + '"' + "value" + '"' + ':' + '"' + Vessel.TonnesMin.ToString() + '"' + '}' + ',' ;
//            }
//            //backspace operation
//            string dataString = dataPreString.Substring(0, dataPreString.Length - 1) ;
//            ViewBag.dataString = dataString + "]";

//            //Chart.js
//            //Chart.js - Options
//            string optionsPreString = "{";
//            optionsPreString += "scaleBeginAtZero: true," +  //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
//                "scaleShowGridLines: true," +       //Boolean - Whether grid lines are shown across the chart
//                "scaleGridLineColor:" + '"' + "rgba(0,0,0,.2)" + '"' + "," +        //String - Colour of the grid lines
//                "scaleGridLineWidth: 1," +          //Number - Width of the grid lines
//                "barShowStroke: true," +            //Boolean - If there is a stroke on each bar
//                "barStrokeWidth: 1," +              //Number - Pixel width of the bar stroke
//                "barValueSpacing: 5," +             //Number - Spacing between each of the X value sets
//                "barDatasetSpacing: 1," +           //Number - Spacing between data sets within X values
//                "responsive: true,";                //Boolean - Re-draw chart on page resize
//                                                    //String - A legend template - not implemented here

//            //"legendTemplate: " + '<' + "ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>" + '"';
//            string optionsString = optionsPreString.Substring(0, optionsPreString.Length - 1);
//            ViewBag.optionsString = optionsString + '}';

//            string labelsPreString = "[";
//            string requiredPreString = "";
//            string optimisedPreString = "";

//            foreach(var vessel in selectedProject.Vessels)
//            {
//                int Seed = 54238;
//                Random rdm = new Random(Seed);
//                float floatRdm = float.Parse(rdm.NextDouble().ToString());
//                labelsPreString += '"' + vessel.Name + '"' + ',';
//                requiredPreString += (vessel.TonnesMin * floatRdm).ToString() + ",";
//                optimisedPreString += vessel.TonnesMin.ToString() + ",";
//            }
//            string labelString = labelsPreString.Substring(0, labelsPreString.Length - 1);
//            string requiredString = requiredPreString.Substring(0, requiredPreString.Length - 1);
//            string optimisedString = optimisedPreString.Substring(0, optimisedPreString.Length - 1);
//            ViewBag.labelString = labelString + "]";
//            ViewBag.requiredString = requiredString;
//            ViewBag.optimisedString = optimisedString;

//            BasicChartsView model = new BasicChartsView();

//            model.optionsView = optionsString + '}';
//            //model.labelView = labelString + "]";
//            //model.requiredView = requiredString;
//            //model.optimisedView = optimisedString;

//            return View(model);
//        }
        
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
        
//    }
    
//}



////Excel Interop

////if (excelTemplate.FileName.EndsWith("xls") || excelTemplate.FileName.EndsWith("xlsx"))
//                //{
//                    //First, save the project details to the projects Table
//                    //Locate a folder on the server and save excel file there, overriding it if is already there.
//                    //string path = Server.MapPath("~/Content/files/TemplateStore/" + excelTemplate.FileName);
//                    //if (System.IO.File.Exists(path))
//                    //    System.IO.File.Delete(path);
//                    //excelTemplate.SaveAs(path);

//                    //Get the data from the excel file
//                    //Excel.Application application = new Excel.Application();

//                    //try to work on the excel file, if this fails, kill the application
//                    //try
//                    //{
//                    //Create a list of vessels
//                    //Excel.Workbook workbook = application.Workbooks.Open(path);
//                    //Excel.Worksheet worksheet = workbook.Sheets["Vessel Requirements"];
//                    //worksheet.Activate();
//                    //worksheet = workbook.ActiveSheet;
//                    //Excel.Range range = worksheet.UsedRange;
//                    //List<Vessel> listVessels = new List<Vessel>();
//                    //for (int row = 2; row <= range.Rows.Count; row++)
//                    //{
//                    //    Vessel v = new Vessel();
//                    //does not need vesselID - autoIncrements
//                    //v.VesselId = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
//                    //v.Name = ((Excel.Range)range.Cells[row, 2]).Text;
//                    //v.Destination = ((Excel.Range)range.Cells[row, 3]).Text;

//                    //v.MoistureMax = float.Parse(((Excel.Range)range.Cells[row, 4]).Text);
//                    //v.CalorificMin = float.Parse(((Excel.Range)range.Cells[row, 5]).Text);
//                    //v.AshMax = float.Parse(((Excel.Range)range.Cells[row, 6]).Text);
//                    //v.VolatileMax = float.Parse(((Excel.Range)range.Cells[row, 7]).Text);
//                    //v.SulfurMax = float.Parse(((Excel.Range)range.Cells[row, 8]).Text);
//                    //v.CalorificBenchMark = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
//                    //v.TonnesMin = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
//                    //v.PricePerTonne = float.Parse(((Excel.Range)range.Cells[row, 10]).Text);
//                    //v.ProjectID = ProjID;
//                    //listVessels.Add(v);
//                    // add vessel to database
//                    //db.Entry(v).State = System.Data.Entity.EntityState.Added;
//                    //}
//                    //TempData["ListVessels"] = listVessels;
//                    //save listVessels to ViewModel list of vessels
//                    //listVessels = p.Vessels.ToList();

//                    //Save the list of vessel to the Vessels Table
//                    //db.SaveChanges();

//                    //Go to the worksheet with the component data
//                    //worksheet = workbook.Sheets["Avaliable Components"];
//                    //worksheet.Activate();
//                    //worksheet = workbook.ActiveSheet;
//                    //range = worksheet.UsedRange;

//                    //create a list of components
//                    //List<Component> ListComponents = new List<Component>();
//                    //for (int row = 2; row <= range.Rows.Count; row++)
//                    //{
//                    //    Component c = new Component();

//                    //    c.Name = ((Excel.Range)range.Cells[row, 2]).Text;

//                    //    c.Tonnes = float.Parse(((Excel.Range)range.Cells[row, 3]).Text);
//                    //    c.MoistureAir = float.Parse(((Excel.Range)range.Cells[row, 4]).Text);
//                    //    c.MoistureTotal = float.Parse(((Excel.Range)range.Cells[row, 5]).Text);
//                    //    c.AshStart = float.Parse(((Excel.Range)range.Cells[row, 6]).Text);
//                    //    c.CalorificStart = float.Parse(((Excel.Range)range.Cells[row, 7]).Text);
//                    //    c.VolatileStart = float.Parse(((Excel.Range)range.Cells[row, 8]).Text);
//                    //    c.SulfurStart = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
//                    //    c.FOBStart = float.Parse(((Excel.Range)range.Cells[row, 10]).Text);
//                    //    c.ProjectID = ProjID;
//                    //    ListComponents.Add(c);
//                    // add component to database
//                    //    db.Entry(c).State = System.Data.Entity.EntityState.Added;
//                    //}
//                    //Transfer the component lists to the View
//                    //TempData["ListComponents"] = ListComponents;
//                    //Overwrite the list of components in the view model with ListComponents (cpview)
//                    //ListComponents = p.Components.ToList();
//                    //TempData["ViewModel"] = p;
//                    //Save the list of components to the Components Table
//                    //db.SaveChanges();

//                    //application.Quit();
//                    //return RedirectToAction("NewProjectCreate", "NewProject", new RouteValueDictionary(p));
//                    //return View(p);
//                    //}
//                    //catch //If anything goes wrong - **** close the excel file   ****
//                    //{
//                    //Close Excel to prevent connection problems
//                    //Good Idea to do this before working with the database
//                    //application.Quit();
//                    //send error message
//                    //        TempData["Error"] = "An error with the excel file has occurred and was caught with the catch clause";
//                    //        return RedirectToAction("NewProjectCreate");
//                    //    }
//                    //} else {

//                    //Send error message
//                    //        TempData["Error"] = "Not an excel file, please try again <br />";
//                    //        return RedirectToAction("NewProjectCreate");
//                    //TempData["Error"] = "Not an excel file, please try again <br />";
//            //return RedirectToAction("NewProjectCreate");



////string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; data source={~/Content/files/TemplateStore/Vessel-Components-Template.xlsx}; Extended Properties=Excel 12.0;";
//////uses - System.Data.OleDb;
////OleDbConnection objConn = null;
////System.Data.DataTable dt = null;
//////Create connection object by using the preceding connection string.
////objConn = new OleDbConnection(ConnectionString);
////objConn.Open();



//// Uses Office.InterOp.Excel - avoid like the plague
////Read the Excel file - Save Data to Vessels List
////Excel.Application application = new Excel.Application();
////Excel.Workbook workbook = application.Workbooks.Open(path);
////Excel.Worksheet worksheet = workbook.Sheets["Vessel Requirements"];
////worksheet.Activate();
////worksheet = workbook.ActiveSheet;
////Excel.Range range = worksheet.UsedRange;
////List<Vessel> listVessels = new List<Vessel>();
////for (int row = 2; row <= range.Rows.Count; row++)
////{
////    Vessel v = new Vessel();
////    v.VesselId = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
////    v.VesselName = ((Excel.Range)range.Cells[row, 2]).Text;
////    v.Destination = ((Excel.Range)range.Cells[row, 3]).Text;

////    v.MoistureTotal = float.Parse(((Excel.Range)range.Cells[row, 4]).Text);
////    v.CalorificRejection = float.Parse(((Excel.Range)range.Cells[row, 5]).Text);
////    v.AshRejection = float.Parse(((Excel.Range)range.Cells[row, 6]).Text);
////    v.VolatileRejection = float.Parse(((Excel.Range)range.Cells[row, 7]).Text);
////    v.SulfurRejection = float.Parse(((Excel.Range)range.Cells[row, 8]).Text);
////    v.CalorificBenchmark = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
////    v.TonnesRequired = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
////    v.PricePerTonne = float.Parse(((Excel.Range)range.Cells[row, 10]).Text);
////    listVessels.Add(v);
////}
////Interop end

////Transfer the vessel list to the View
////ViewBag.ListVessels = listVessels;

////Go to the worksheet with the component data
////worksheet = workbook.Sheets[2].Activate();
////worksheet = workbook.Sheets["Available Components"];
////worksheet.Activate();
////worksheet = workbook.ActiveSheet;
////range = worksheet.UsedRange;
////List<Component> ListComponents = new List<Component>();

////Uses Interop - <plague>
////for (int row = 2; row <= range.Rows.Count; row++)
////{
////    Component c = new Component();
////    c.ComponentID = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
////    c.ComponentName = ((Excel.Range)range.Cells[row, 2]).Text;

////    c.ComponentTonnesStart = float.Parse(((Excel.Range)range.Cells[row, 3]).Text);
////    c.MoistureAirDriedStart = float.Parse(((Excel.Range)range.Cells[row, 4]).Text);
////    c.MoistureTotalStart = float.Parse(((Excel.Range)range.Cells[row, 5]).Text);
////    c.AshStart = float.Parse(((Excel.Range)range.Cells[row, 6]).Text);
////    c.CalorificValueStart = float.Parse(((Excel.Range)range.Cells[row, 7]).Text);
////    c.VolatileStart = float.Parse(((Excel.Range)range.Cells[row, 8]).Text);
////    c.SulfurStart = float.Parse(((Excel.Range)range.Cells[row, 9]).Text);
////    c.FOBStart = float.Parse(((Excel.Range)range.Cells[row, 10]).Text);
////    ListComponents.Add(c);
////}
//// Interop end </plague>


////ViewBag.ListComponents = ListComponents;

////Close Excel to prevent connection problems
////Good Idea to do this before working with the database
////application.Quit();


////[HttpPost]
////public ActionResult TemplateUpload(HttpPostedFileBase excelfile)
////{
////    //GemBox method
////    //Set the license key for GEM Box here
////    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
////    if (excelfile == null || excelfile.ContentLength == 0)
////    {
////        ViewBag.Error = "File has no data <br />";
////        return View("NewProjectIndex");
////    }
////    else {
////        if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
////        {
////            string path = Server.MapPath("~/Content/files/TemplateStore/" + excelfile.FileName);
////            if (System.IO.File.Exists(path))
////                System.IO.File.Delete(path);
////            excelfile.SaveAs(path);

////            //Uses GemBox.Spreadsheet
////            ExcelFile clientTemplate = ExcelFile.Load(path);
////            DataTable datatable = new DataTable();

////            //Select the vessels worksheet from the file
////            ExcelWorksheet vesselsSheet = clientTemplate.Worksheets["Vessel Requirements"];
////            int rowCount = vesselsSheet.Rows.Count - 1;

////            List<Vessel> listVessels = new List<Vessel>();

////            var columnCount = vesselsSheet.CalculateMaxUsedColumns();

////            for (int row = 1; row <= rowCount; row++)
////            {
////                Vessel v = new Vessel();
////                v.VesselID = int.Parse(vesselsSheet.Cells[row, 0].GetFormattedValue());
////                v.Name = vesselsSheet.Cells[row, 1].GetFormattedValue();
////                v.Destination = vesselsSheet.Cells[row, 2].GetFormattedValue();
////                v.MoistureMax = float.Parse(vesselsSheet.Cells[row, 3].GetFormattedValue());
////                v.AshMax = float.Parse(vesselsSheet.Cells[row, 4].GetFormattedValue());
////                v.CalorificMin = float.Parse(vesselsSheet.Cells[row, 5].GetFormattedValue());
////                v.VolatileMax = float.Parse(vesselsSheet.Cells[row, 6].GetFormattedValue());
////                v.SulfurMax = float.Parse(vesselsSheet.Cells[row, 7].GetFormattedValue());
////                v.PricePerTonne = float.Parse(vesselsSheet.Cells[row, 8].GetFormattedValue());
////                v.TonnesMin = float.Parse(vesselsSheet.Cells[row, 9].GetFormattedValue());
////                v.CalorificBenchMark = float.Parse(vesselsSheet.Cells[row, 10].GetFormattedValue());
////                listVessels.Add(v);
////            }

////            ViewBag.ListVessels = listVessels;

////            //Select the vessels worksheet from the file
////            ExcelWorksheet componentsSheet = clientTemplate.Worksheets["Avaliable Components"];
////            int rowCountC = componentsSheet.Rows.Count - 1;


////            List<Component> listComponents = new List<Component>();
////            for (int row = 1; row <= rowCountC; row++)
////            {
////                Component c = new Component();
////                c.ComponentID = int.Parse(componentsSheet.Cells[row, 0].GetFormattedValue());
////                c.Name = componentsSheet.Cells[row, 1].GetFormattedValue();

////                c.Tonnes = float.Parse(componentsSheet.Cells[row, 2].GetFormattedValue());
////                c.MoistureAir = float.Parse(componentsSheet.Cells[row, 3].GetFormattedValue());
////                c.MoistureTotal = float.Parse(componentsSheet.Cells[row, 4].GetFormattedValue());
////                c.AshStart = float.Parse(componentsSheet.Cells[row, 5].GetFormattedValue());
////                c.CalorificStart = float.Parse(componentsSheet.Cells[row, 6].GetFormattedValue());
////                c.VolatileStart = float.Parse(componentsSheet.Cells[row, 7].GetFormattedValue());
////                c.SulfurStart = float.Parse(componentsSheet.Cells[row, 8].GetFormattedValue());
////                c.FOBStart = float.Parse(componentsSheet.Cells[row, 9].GetFormattedValue());
////                listComponents.Add(c);
////            }

////            ViewBag.ListComponents = listComponents;

////            //End the excel application
////            //foreach (Process process in Process.GetProcessesByName("Excel"))
////            //{
////            //    process.Kill();
////            //}

////            return View("UploadSuccess");
////        }
////        else {
////            ViewBag.Error = "Not an excel file <br />";
////            return View("NewProjectIndex");
////        }
////    }
////}