using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.Model
{
    public class Component
    {
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public float ComponentTonnesStart { get; set; }
        public float MoistureAirDriedStart { get; set; }
        public float MoistureTotalStart { get; set; }
        public float AshStart { get; set; }
        public float CalorificValueStart { get; set; }
        public float VolatileStart { get; set; }
        public float SulfurStart { get; set; }
        public float FOBStart { get; set; }
    }
}
