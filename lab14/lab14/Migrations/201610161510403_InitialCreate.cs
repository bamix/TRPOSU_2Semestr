namespace lab14.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoodId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodId, cascadeDelete: true)
                .Index(t => t.GoodId);
            
            CreateTable(
                "dbo.ResourceGoods",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        Good_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.Good_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.Goods", t => t.Good_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.Good_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "GoodId", "dbo.Goods");
            DropForeignKey("dbo.ResourceGoods", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.ResourceGoods", "Resource_Id", "dbo.Resources");
            DropIndex("dbo.ResourceGoods", new[] { "Good_Id" });
            DropIndex("dbo.ResourceGoods", new[] { "Resource_Id" });
            DropIndex("dbo.Sales", new[] { "GoodId" });
            DropTable("dbo.ResourceGoods");
            DropTable("dbo.Sales");
            DropTable("dbo.Resources");
            DropTable("dbo.Goods");
        }
    }
}
