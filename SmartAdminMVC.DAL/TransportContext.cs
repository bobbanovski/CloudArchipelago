using SmartAdminMvc.ArchipelagoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdminMvc.DAL
{
    public class TransportContext : DbContext
    {
        public TransportContext() : base("ArchipelagoDB") { }

        public DbSet<TransProject> TransProjects { get; set; }
        public DbSet<Citie> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
