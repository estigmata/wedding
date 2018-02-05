namespace JalaFoundation.Dev23.Wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CouplePersonAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        CoupleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Couples", t => t.CoupleID, cascadeDelete: true)
                .Index(t => t.CoupleID);
            
            CreateTable(
                "dbo.Couples",
                c => new
                    {
                        CoupleID = c.Int(nullable: false, identity: true),
                        WeddingDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        WifeID = c.Int(),
                        HusbandID = c.Int(),
                    })
                .PrimaryKey(t => t.CoupleID)
                .ForeignKey("dbo.People", t => t.HusbandID)
                .ForeignKey("dbo.People", t => t.WifeID)
                .Index(t => t.WifeID)
                .Index(t => t.HusbandID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CI = c.Int(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Direction = c.String(),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.PersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "CoupleID", "dbo.Couples");
            DropForeignKey("dbo.Couples", "WifeID", "dbo.People");
            DropForeignKey("dbo.Couples", "HusbandID", "dbo.People");
            DropIndex("dbo.Couples", new[] { "HusbandID" });
            DropIndex("dbo.Couples", new[] { "WifeID" });
            DropIndex("dbo.Accounts", new[] { "CoupleID" });
            DropTable("dbo.People");
            DropTable("dbo.Couples");
            DropTable("dbo.Accounts");
        }
    }
}
