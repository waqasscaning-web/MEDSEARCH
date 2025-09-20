namespace OMS.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202007311054529_twocontextreset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.subEmails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.subEmails");
        }
    }
}
