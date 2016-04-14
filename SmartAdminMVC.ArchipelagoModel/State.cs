using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.ArchipelagoModel
{
    public class State
    {
        public int StateID { get; set; }
        public int TransProjectID { get; set; }

        public string Name { get; set; }
        public int Demand { get; set; }

        public virtual TransProject TransProject { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}
