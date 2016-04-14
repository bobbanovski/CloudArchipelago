using SmartAdminMvc.DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdminMvc.ArchipelagoModel;
using System.Globalization;
using System.IO;
using GemBox.Spreadsheet;
using System.Data;
using System.Diagnostics;
using Microsoft.SolverFoundation.Services;
using SmartAdminMvc.ArchipelagoModel.ViewModels;

namespace SmartAdminMvc.Controllers.Business
{
    [Authorize]
    public class TransportOptController : Controller
    {
        TransportContext db = new TransportContext();

        public ActionResult TransportIndex()
        {
            var projectList = db.TransProjects.ToList();

            if (TempData["Flag"] != null)
                {
                    string F = TempData["Flag"].ToString();
                    ViewBag.Flag = F;
                }
            if (TempData["Error"] != null)
            {
                string E = TempData["Error"].ToString();
                ViewBag.Error = E;
            }
                return View(projectList);
        }
        
        public ActionResult TransportCreate()
        {
            DateTime currentTime = DateTime.Now;
            var culture = new CultureInfo("en-GB");
            string savedtime = currentTime.ToString(culture);
            ViewBag.CurrentTime = savedtime;

            var viewModel = new TransProject();

            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult TransportCreate(string ProjectName, string ProjectDate)
        {
            TransProject transProject = new TransProject();
            transProject.Name = ProjectName;
            transProject.Date = ProjectDate;
            db.Entry(transProject).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            //Save excel file from form to an object
            HttpPostedFileBase excelTemplate = Request.Files["excelfile"];
            
            //GemBox method
            //Set the license key for GEM Box here
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            if (excelTemplate == null || excelTemplate.ContentLength == 0)
            {
                TempData["Error"] = "File has no data <br />";
                return RedirectToAction("TransportIndex");
            }else {
                if (excelTemplate.FileName.EndsWith("xls") || excelTemplate.FileName.EndsWith("xlsx"))
                {
                    //Locate a folder on the server and save excel file there, overriding it if is already there.
                    string path = Server.MapPath("~/Content/files/Upload/" + excelTemplate.FileName);

                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    excelTemplate.SaveAs(path);

                    //Uses GemBox.Spreadsheet
                    ExcelFile clientTemplate = ExcelFile.Load(path);
                    DataTable datatable = new DataTable();

                    //Select the Parameters worksheet from the file
                    ExcelWorksheet inputSheet = clientTemplate.Worksheets["Parameters"];
                    int rowCount = inputSheet.Rows.Count - 1;

                    
                    List<Citie> listCities = new List<Citie>();

                    //Get the new projectID
                    int ProjID = db.TransProjects.Max(projid => projid.TransprojectID);

                    var columnCount = inputSheet.CalculateMaxUsedColumns();
                    //inflexible code
                    for (int row = 1; row < rowCount; row++)
                    {
                        Citie c = new Citie();
                        c.Name = inputSheet.Cells[row, 0].GetFormattedValue();
                        c.Capacity = int.Parse(inputSheet.Cells[row, 19].GetFormattedValue());
                        c.TransProjectID = ProjID;
                        listCities.Add(c);
                        db.Entry(c).State = EntityState.Added;
                        
                    }
                    //db.SaveChanges();
                    

                    List<State> listStates = new List<State>();
                    for (int col = 1; col < 19; col++)
                    {
                        State s = new State();
                        s.Name = inputSheet.Cells[0, col].GetFormattedValue();
                        s.Demand = int.Parse(inputSheet.Cells[18, col].GetFormattedValue());
                        s.TransProjectID = ProjID;
                        listStates.Add(s);
                        db.Entry(s).State = EntityState.Added;
                        
                    }
                    db.SaveChanges();
                    //Inflexible code
                    int CitId = db.Cities.Max(citid => citid.CitieID) - 18;
                    int StID = db.States.Max(stid => stid.StateID) - 18;

                    List<Unit> listUnits = new List<Unit>();
                    for (int row = 1; row < 19; row++)
                    {
                        for(int col = 1; col < 19; col++)
                        {
                            Unit u = new Unit();
                            u.CitieID = CitId + row;
                            u.StateID = StID + col;
                            u.Project = ProjID;
                            u.SupplyNode = inputSheet.Cells[row, 0].GetFormattedValue();
                            u.DemandNode = inputSheet.Cells[0, col].GetFormattedValue();
                            u.TransportCost = int.Parse(inputSheet.Cells[row, col].GetFormattedValue());
                            listUnits.Add(u);
                            db.Entry(u).State = EntityState.Added;
                            db.SaveChanges();
                        }
                    }
                    //save database changes
                    

                    //End the excel application
                    foreach (Process process in Process.GetProcessesByName("Excel"))
                    {
                        process.Kill();
                    }

                    

                    //Optimisation Time
                    //Model string for Solver Foundation
                    string strModel = @"Model[
				        Parameters[Sets,Source,Sink],
				        Parameters[Reals,Supply[Source],Demand[Sink],Cost[Source,Sink]],
				        Decisions[Reals[0,Infinity],flow[Source,Sink],TotalCost],
				        Constraints[
					        TotalCost == Sum[{i,Source},{j,Sink},Cost[i,j]*flow[i,j]],
					        Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]<=Supply[i]],
					        Foreach[{j,Sink}, Sum[{i,Source},flow[i,j]]>=Demand[j]]],
				        Goals[Minimize[TotalCost]] ]";

                    // Load OML-Model
                    SolverContext TrasOptcontext = SolverContext.GetContext();
                    TrasOptcontext.LoadModel(FileFormat.OML, new StringReader(strModel));
                    TrasOptcontext.CurrentModel.Name = "Transportation Problem";

                    // Create Tables
                    // Supply table
                    DataTable pSupply = new DataTable();
                    pSupply.Columns.Add("SupplyNode", Type.GetType("System.String"));
                    pSupply.Columns.Add("Supply", Type.GetType("System.Int32"));

                    foreach (var city in listCities)
                    {
                        DataRow supplyRow = pSupply.NewRow();
                        supplyRow["SupplyNode"] = city.Name;
                        supplyRow["Supply"] = city.Capacity;
                        pSupply.Rows.Add(supplyRow);
                    }

                    // Demand table
                    DataTable pDemand = new DataTable();
                    pDemand.Columns.Add("DemandNode", Type.GetType("System.String"));
                    pDemand.Columns.Add("Demand", Type.GetType("System.Int32"));

                    foreach (var state in listStates)
                    {
                        DataRow demandRow = pDemand.NewRow();
                        demandRow["DemandNode"] = state.Name;
                        demandRow["Demand"] = state.Demand;
                        pDemand.Rows.Add(demandRow);
                    }

                    // OD-Matrix
                    DataTable pCost = new DataTable();
                    pCost.Columns.Add("SupplyNode", Type.GetType("System.String"));
                    pCost.Columns.Add("DemandNode", Type.GetType("System.String"));
                    pCost.Columns.Add("Cost", Type.GetType("System.Double"));

                    foreach (var unit in listUnits)
                    {
                        DataRow unitRow = pCost.NewRow();
                        unitRow["SupplyNode"] = unit.SupplyNode;
                        unitRow["DemandNode"] = unit.DemandNode;
                        unitRow["Cost"] = unit.TransportCost;
                        pCost.Rows.Add(unitRow);
                    }

                    // Bind values from tables to parameter of the OML model
                    foreach (Parameter par in TrasOptcontext.CurrentModel.Parameters)
                    {
                        switch (par.Name)
                        {
                            case "Supply":
                                par.SetBinding(pSupply.AsEnumerable(), "Supply", "SupplyNode");
                                break;
                            case "Demand":
                                par.SetBinding(pDemand.AsEnumerable(), "Demand", "DemandNode");
                                break;
                            case "Cost":
                                par.SetBinding(pCost.AsEnumerable(), "Cost", "SupplyNode", "DemandNode");
                                break;
                        }
                    }

                    // Call solver
                    Solution solution = TrasOptcontext.Solve();
                    Report report = solution.GetReport();
                    double cost = 0;
                    foreach (Decision desc in solution.Decisions)
                    {
                        if (desc.Name == "TotalCost")
                        {
                            foreach (object[] value in desc.GetValues())
                            {
                                cost = Convert.ToDouble(value[0]);
                            }
                        }
                    }

                    transProject.MinimisedCost = cost;
                    db.Entry(transProject).State = EntityState.Modified;
                    db.SaveChanges();
                    // Print out optimized results
                    string result = String.Empty;
                    //double totalFlow = 0.0;
                    foreach (Decision desc in solution.Decisions)
                    {
                        // flow as variable
                        if (desc.Name == "flow")
                        {
                            foreach (object[] value in desc.GetValues())
                            {
                                //values to go into database
                                string source = value[1].ToString();
                                string sink = value[2].ToString();
                                double flow = Convert.ToDouble(value[0]);

                                Unit unit = db.Units
                                    .Where(u => u.Project == ProjID &&
                                    u.SupplyNode == source &&
                                    u.DemandNode == sink)
                                    .SingleOrDefault();

                                unit.Supplied = flow;
                                db.Entry(unit).State = EntityState.Modified;
                                db.SaveChanges(); //Has to be inside the loop or changes aren't saved
                            }
                            
                            // lookup km from arcs table
                            //DataRow[] rows = new DataRow[1];
                            //rows = pCost.Select("SupplyNode ='" + source + "' AND DemandNode ='" + sink + "'");
                            //double km = Convert.ToDouble(rows[0]["Cost"]);
                            //string sourceSink = String.Format("{0}_{1}", source, sink);
                            //if (flow != 0)
                            //{
                            //    totalFlow += flow;
                            //    result = result + "\n" + String.Format("\"{0}\";\"{1}\";\"{2}\";{3};{4}",
                            //                                           sourceSink, source, sink, flow, km);
                            //}

                            //Console.WriteLine(result);
                        }
                    }
                    TempData["Flag"] = "Template Uploaded and Processed";
                }
                else {
                    ViewBag.Error = "Not an excel file <br />";
                    return RedirectToAction("TransportIndex");
                }
            }
            return RedirectToAction("TransportIndex");
        }
        
        public ActionResult TransportView(int id)
        {
            //Lazy loading
            //var workingProject = db.TransProjects.Find(id);
            //var workingCitie = db.Cities
            //    .Where(c => c.TransProjectID == id);
            //Eager loading
            //var workingProject = db.TransProjects
            //    .Include(c => c.Cities)
            //    .Include(s => s.States)
            //    .Where(i => i.TransprojectID == id)
            //    .Single();

            var viewModel = new TransportViewModel();
            viewModel.TransProjectView = db.TransProjects.Where(i => i.TransprojectID == id).Single();
            viewModel.CitieView = db.Cities.Where(i => i.TransProjectID == id).ToList();
            viewModel.StateView = db.States.Where(i => i.TransProjectID == id).ToList();

            return PartialView(viewModel);
        }

        [HttpGet]
        public ActionResult TransportDelete(int id)
        {
            var deletedProject = db.TransProjects.Find(id);
            //db.Entry(deletedProject).State = EntityState.Deleted;
            return PartialView(deletedProject);
        }
        
        [HttpPost, ActionName("TransportDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult TransportDeleteConfirmed(int id)
        {
            TransProject deleteProject = db.TransProjects.Find(id);
            db.TransProjects.Remove(deleteProject);
            db.SaveChanges();
            ViewBag.Flag = "Project Deleted";
            return RedirectToAction("TransportIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}