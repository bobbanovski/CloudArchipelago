//using SmartAdminMvc.ArchipelagoModel;
//using SmartAdminMvc.DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Data.Entity;
//using GemBox.Spreadsheet;
//using System.Data;

//namespace SmartAdminMvc.Controllers.Business
//{
//    [Authorize]
//    public class ProjectWizardController : Controller
//    {
//        // GET: ProjectWizard
//        public ActionResult NewProjectWizard()
//        {
//            // Invokes database
//            //OptimiseContext db = new OptimiseContext();

            

//            // Does NOT invoke database
//            List<Vessel> ListVessels = new List<Vessel>();
//            List<Component> ListComponents = new List<Component>();
//            ViewBag.ListVessels = ListVessels;
//            ViewBag.ListComponents = ListComponents;

//            //Return Projects model in database to View
//            return View();
//        }

//        [HttpPost]
//        public ActionResult TemplateUpload(HttpPostedFileBase excelfile)
//        {
//            //GemBox method
//            //Set the license key for GEM Box here
//            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
//            if (excelfile == null || excelfile.ContentLength == 0)
//            {
//                ViewBag.Error = "File has no data <br />";
//                return View("NewProjectIndex");
//            }
//            else {
//                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
//                {
//                    string path = Server.MapPath("~/Content/files/TemplateStore/" + excelfile.FileName);
//                    if (System.IO.File.Exists(path))
//                        System.IO.File.Delete(path);
//                    excelfile.SaveAs(path);

//                    //Uses GemBox.Spreadsheet
//                    ExcelFile clientTemplate = ExcelFile.Load(path);
//                    DataTable datatable = new DataTable();

//                    //Select the vessels worksheet from the file
//                    ExcelWorksheet vesselsSheet = clientTemplate.Worksheets["Vessel Requirements"];
//                    int rowCount = vesselsSheet.Rows.Count - 1;

//                    List<Vessel> listVessels = new List<Vessel>();

//                    var columnCount = vesselsSheet.CalculateMaxUsedColumns();

//                    for (int row = 1; row <= rowCount; row++)
//                    {
//                        Vessel v = new Vessel();
//                        
//                        listVessels.Add(v);
//                    }

//                    ViewBag.ListVessels = listVessels;

//                    //Select the vessels worksheet from the file
//                    ExcelWorksheet componentsSheet = clientTemplate.Worksheets["Avaliable Components"];
//                    int rowCountC = componentsSheet.Rows.Count - 1;


//                    List<Component> listComponents = new List<Component>();
//                    for (int row = 1; row <= rowCountC; row++)
//                    {
//                        Component c = new Component();
//                        
//                        listComponents.Add(c);
//                    }

//                    ViewBag.ListComponents = listComponents;

//                    //End the excel application
//                    //foreach (Process process in Process.GetProcessesByName("Excel"))
//                    //{
//                    //    process.Kill();
//                    //}

//                    return View();
//                }
//                else {
//                    ViewBag.Error = "Not an excel file <br />";
//                    return View("NewProjectIndex");
//                }
//            }
            
//        }
//    }
//}