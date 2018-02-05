namespace JalaFoundation.Dev23.Wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class presentList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PresentLists",
                c => new
                    {
                        PresentListID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PresentListID);
            
            AddColumn("dbo.Couples", "PresentListID", c => c.Int());
            CreateIndex("dbo.Couples", "PresentListID");
            AddForeignKey("dbo.Couples", "PresentListID", "dbo.PresentLists", "PresentListID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Couples", "PresentListID", "dbo.PresentLists");
            DropIndex("dbo.Couples", new[] { "PresentListID" });
            DropColumn("dbo.Couples", "PresentListID");
            DropTable("dbo.PresentLists");
        }
    }
}
