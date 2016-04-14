using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Services;
using System.IO;

namespace SmartAdminMvc.SolverEngine
{
    public class TransportSolv
    {
        private string strModel = @"Model[Parameters[Sets,Source,Sink],
                Parameters[Reals,Supply[Source],Demand[Sink],Cost[Source,Sink]],
                Decisions[Reals[0,Infinity],flow[Source,Sink],TotalCost],
                Constraints[TotalCost == Sum[{i,Source},{j,Sink},Cost[i,j]*flow[i,j]],
                    Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]<=Supply[i]],
                    Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]>=Demand[j]]],
                Goals[Minimize[TotalCost]] ]";

        public void TransportOptimisation()
        {
            
            //Load the model into Solver Foundation
            SolverContext transportSolv = SolverContext.GetContext();
            transportSolv.LoadModel(FileFormat.OML, new StringReader(strModel));
            transportSolv.CurrentModel.Name = "TransportOptimisation";

            List<State>
        }

        
    }
}
