namespace JalaFoundation.Dev23.Wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dedicatory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dedicatories",
                c => new
                    {
                        DedicatoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Message = c.String(),
                        PresentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DedicatoryID)
                .ForeignKey("dbo.Presents", t => t.PresentID, cascadeDelete: true)
                .Index(t => t.PresentID);
            
            CreateTable(
                "dbo.Presents",
                c => new
                    {
                        PresentID = c.Int(nullable: false, identity: true),
                        PresentListID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PresentID)
                .ForeignKey("dbo.PresentLists", t => t.PresentListID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.PresentListID)
                .Index(t => t.ProductID);
            
            AddColumn("dbo.Accounts", "Token", c => c.String());
            AddColumn("dbo.Couples", "Latitude", c => c.Single(nullable: false));
            AddColumn("dbo.Couples", "Longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dedicatories", "PresentID", "dbo.Presents");
            DropForeignKey("dbo.Presents", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Presents", "PresentListID", "dbo.PresentLists");
            DropIndex("dbo.Presents", new[] { "ProductID" });
            DropIndex("dbo.Presents", new[] { "PresentListID" });
            DropIndex("dbo.Dedicatories", new[] { "PresentID" });
            DropColumn("dbo.Couples", "Longitude");
            DropColumn("dbo.Couples", "Latitude");
            DropColumn("dbo.Accounts", "Token");
            DropTable("dbo.Presents");
            DropTable("dbo.Dedicatories");
        }
    }
}
