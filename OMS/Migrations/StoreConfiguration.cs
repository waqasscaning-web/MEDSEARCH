namespace OMS.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class StoreConfiguration : DbMigrationsConfiguration<OMS.DAL.MedicalContext>
    {
        public StoreConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "OMS.DAL.MedicalContext";
        }

        protected override void Seed(OMS.DAL.MedicalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
