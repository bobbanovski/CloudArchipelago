using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.Model
{
    public class Project
    {
        //Primary Key
        public int ProjectID { get; set; }
        public DateTime DateCreated { get; set; }

        //Foreign Keys
        public int VesselID { get; set; }
        public int ComponentID { get; set; }

        //Navigational Properties
        public virtual Vessel Vessel { get; set; }
        public virtual Component Component { get; set; }
    }
}
