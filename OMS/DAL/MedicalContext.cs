using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OMS.DAL
{
    public class MedicalContext : DbContext
    {
        public System.Data.Entity.DbSet<OMS.Models.Products> Products { get; set; }
        public System.Data.Entity.DbSet<OMS.Models.subEmails> sub { get; set; }
        public System.Data.Entity.DbSet<OMS.Models.MadicalStore> MadicalStores { get; set; }

    }
}