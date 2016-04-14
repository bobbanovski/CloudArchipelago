namespace SmartAdminMvc.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CitieID = c.Int(nullable: false, identity: true),
                        TransProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CitieID)
                .ForeignKey("dbo.TransProjects", t => t.TransProjectID, cascadeDelete: true)
                .Index(t => t.TransProjectID);
            
            CreateTable(
                "dbo.TransProjects",
                c => new
                    {
                        TransprojectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.String(),
                        MinimisedCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TransprojectID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        TransProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        Demand = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.TransProjects", t => t.TransProjectID, cascadeDelete: true)
                .Index(t => t.TransProjectID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        Project = c.Int(nullable: false),
                        CitieID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        SupplyNode = c.String(),
                        DemandNode = c.String(),
                        Supplied = c.Double(nullable: false),
                        TransportCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UnitID)
                .ForeignKey("dbo.Cities", t => t.CitieID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.CitieID)
                .Index(t => t.StateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "StateID", "dbo.States");
            DropForeignKey("dbo.Units", "CitieID", "dbo.Cities");
            DropForeignKey("dbo.States", "TransProjectID", "dbo.TransProjects");
            DropForeignKey("dbo.Cities", "TransProjectID", "dbo.TransProjects");
            DropIndex("dbo.Units", new[] { "StateID" });
            DropIndex("dbo.Units", new[] { "CitieID" });
            DropIndex("dbo.States", new[] { "TransProjectID" });
            DropIndex("dbo.Cities", new[] { "TransProjectID" });
            DropTable("dbo.Units");
            DropTable("dbo.States");
            DropTable("dbo.TransProjects");
            DropTable("dbo.Cities");
        }
    }
}
