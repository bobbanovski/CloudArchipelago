using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.ArchipelagoModel
{
    public class Citie
    {
        public int CitieID { get; set; }
        //[ForeignKey("TransProject")]
        public int TransProjectID { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public virtual TransProject Transproject { get; set; }
        public virtual ICollection<Unit> Unit { get; set; }
    }
}
