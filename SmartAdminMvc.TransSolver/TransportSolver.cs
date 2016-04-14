using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Services;
using System.IO;
using SmartAdminMvc.ArchipelagoModel;
using System.Data;

namespace SmartAdminMvc.TransSolver
{
    public class TransportSolver
    {
        private string strModel = @"Model[Parameters[Sets,Source,Sink],
                Parameters[Reals,Supply[Source],Demand[Sink],Cost[Source,Sink]],
                Decisions[Reals[0,Infinity],flow[Source,Sink],TotalCost],
                Constraints[TotalCost == Sum[{i,Source},{j,Sink},Cost[i,j]*flow[i,j]],
                    Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]<=Supply[i]],
                    Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]>=Demand[j]]],
                Goals[Minimize[TotalCost]] ]";

        public void TransportOptimisation(int projectID)
        {

            //Load the model into Solver Foundation
            SolverContext transportSolv = SolverContext.GetContext();
            transportSolv.LoadModel(FileFormat.OML, new StringReader(strModel));
            transportSolv.CurrentModel.Name = "TransportOptimisation";

            // Create Tables
            // Supply table
            DataTable pSupply = new DataTable();
            pSupply.Columns.Add("SupplyNode", Type.GetType("System.String"));
            pSupply.Columns.Add("Supply", Type.GetType("System.Int32"));

            // Demand table
            DataTable pDemand = new DataTable();
            pDemand.Columns.Add("DemandNode", Type.GetType("System.String"));
            pDemand.Columns.Add("Demand", Type.GetType("System.Int32"));

            // OD-Matrix
            DataTable pCost = new DataTable();
            pCost.Columns.Add("SupplyNode", Type.GetType("System.String"));
            pCost.Columns.Add("DemandNode", Type.GetType("System.String"));
            pCost.Columns.Add("Cost", Type.GetType("System.Double"));

            
            //add values from database to lists and convert to string


            //Feed lists into solver
            foreach (Parameter p in transportSolv.CurrentModel.Parameters)
            {
                switch (p.Name)
                {
                    case "Supply":
                        p.SetBinding(pSupply.AsEnumerable(), "Supply", "SupplyNode");
                        break;
                    case "Demand":
                        p.SetBinding(pDemand.AsEnumerable(), "Demand", "DemandNode");
                        break;
                    case "Cost":
                        p.SetBinding(pCost.AsEnumerable(), "Cost", "SupplyNode", "DemandNode");
                        break;
                }
            }

            //Call the solver, determine minimised cost
            Solution solution = transportSolv.Solve();
            Report report = solution.GetReport();
            double cost = 0;
            foreach(Decision desc in solution.Decisions){
                if(desc.Name == "TotalCost") {
                    foreach (object[] value in desc.GetValues())
                    {
                        cost = Convert.ToDouble(value[0]);
                    }
                }
            }

            // Print out optimized results
            //string result = String.Empty;
            //double totalFlow = 0.0;
            //foreach (Decision desc in solution.Decisions)
            //{
            //    // flow as variable
            //    if (desc.Name == "flow")
            //    {
            //        foreach (object[] value in desc.GetValues())
            //        {
            //            string source = value[1].ToString();
            //            string sink = value[2].ToString();
            //            double flow = Convert.ToDouble(value[0]);

            //            // lookup km from arcs table
            //            DataRow[] rows = new DataRow[1];
            //            rows = pCost.Select("SupplyNode ='" + source + "' AND DemandNode ='" + sink + "'");
            //            double km = Convert.ToDouble(rows[0]["Cost"]);
            //            string sourceSink = String.Format("{0}_{1}", source, sink);
            //            if (flow != 0)
            //            {
            //                totalFlow += flow;
            //                result = result + "\n" + String.Format("\"{0}\";\"{1}\";\"{2}\";{3};{4}",
            //                                                       sourceSink, source, sink, flow, km);
            //            }
            //        }
            //        Console.WriteLine(result);
            //    }
            //}
            ;
        }
    }
}
