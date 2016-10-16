namespace lab14.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "GoodId", "dbo.Goods");
            DropIndex("dbo.Sales", new[] { "GoodId" });
            RenameColumn(table: "dbo.Sales", name: "GoodId", newName: "Good_Id");
            AlterColumn("dbo.Sales", "Good_Id", c => c.Int());
            CreateIndex("dbo.Sales", "Good_Id");
            AddForeignKey("dbo.Sales", "Good_Id", "dbo.Goods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Good_Id", "dbo.Goods");
            DropIndex("dbo.Sales", new[] { "Good_Id" });
            AlterColumn("dbo.Sales", "Good_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Sales", name: "Good_Id", newName: "GoodId");
            CreateIndex("dbo.Sales", "GoodId");
            AddForeignKey("dbo.Sales", "GoodId", "dbo.Goods", "Id", cascadeDelete: true);
        }
    }
}
