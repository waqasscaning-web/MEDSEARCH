namespace OMS.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twocontextreset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MadicalStores",
                c => new
                    {
                        MadicalStoreID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MobileNo = c.String(),
                        StoreName = c.String(),
                        StoreLicense = c.String(),
                        City = c.String(),
                        LandLine = c.String(),
                        StoreAddress = c.String(),
                        userid = c.String(),
                        StoreImage = c.String(),
                    })
                .PrimaryKey(t => t.MadicalStoreID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProducName = c.String(nullable: false),
                        Producformula = c.String(nullable: false),
                        Productweight = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        productImage = c.String(),
                        MadicalStoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MadicalStores", t => t.MadicalStoreID)
                .Index(t => t.MadicalStoreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MadicalStoreID", "dbo.MadicalStores");
            DropIndex("dbo.Products", new[] { "MadicalStoreID" });
            DropTable("dbo.Products");
            DropTable("dbo.MadicalStores");
        }
    }
}
