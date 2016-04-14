using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.ArchipelagoModel.ViewModels
{
    public class TransportViewModel
    {
        public TransProject TransProjectView { get; set; }
        public ICollection<Citie> CitieView { get; set; }
        public ICollection<State> StateView { get; set; }
        public ICollection<Unit> UnitView { get; set; }
    }
}
