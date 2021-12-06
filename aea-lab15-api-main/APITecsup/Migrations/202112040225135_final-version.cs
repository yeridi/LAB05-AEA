namespace APITecsup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalversion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "ClientID", "dbo.Clients");
            DropIndex("dbo.Invoices", new[] { "ClientID" });
            DropColumn("dbo.Invoices", "ClientID");
            DropColumn("dbo.Details", "Igv");
            DropColumn("dbo.Details", "Total");
            DropTable("dbo.Clients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            AddColumn("dbo.Details", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Details", "Igv", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "ClientID", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "ClientID");
            AddForeignKey("dbo.Invoices", "ClientID", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
