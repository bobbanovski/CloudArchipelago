using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.ArchipelagoModel
{
    public class TransProject
    {
        public int TransprojectID { get; set; }
        public string Name { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }

        public double MinimisedCost { get; set; }

        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Citie> Cities { get; set; }
        //public virtual ICollection<Unit> Units { get; set; }
    }
}
