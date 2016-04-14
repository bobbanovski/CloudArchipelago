using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.Model
{
    public class Vessel
    {
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public string Destination { get; set; }

        public float MoistureTotal { get; set; }
        public float CalorificRejection { get; set; }
        public float AshRejection { get; set; }
        public float VolatileRejection { get; set; }
        public float SulfurRejection { get; set; }
        public float CalorificBenchmark { get; set; }
        public float TonnesRequired { get; set; }
        public float PricePerTonne { get; set; }
    }
}
