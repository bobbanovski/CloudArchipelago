using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.ArchipelagoModel
{
    public class Unit
    {
        public int UnitID { get; set; }
        
        public int Project { get; set; }

        public int CitieID { get; set; }
        public int StateID { get; set; }
        
        public string SupplyNode { get; set; }
        public string DemandNode { get; set; }

        public double Supplied { get; set; }
        public double TransportCost { get; set; }

        public virtual State State { get; set; }
        public virtual Citie Citie { get; set; }
        //public virtual TransProject TransProject { get; set; }
    }
}
