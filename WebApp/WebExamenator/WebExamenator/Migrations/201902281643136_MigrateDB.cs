namespace WebExamenator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TextTasks", "Examen_Id", "dbo.Examen");
            DropIndex("dbo.TextTasks", new[] { "Examen_Id" });
            DropColumn("dbo.TextTasks", "Examen_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextTasks", "Examen_Id", c => c.Int());
            CreateIndex("dbo.TextTasks", "Examen_Id");
            AddForeignKey("dbo.TextTasks", "Examen_Id", "dbo.Examen", "Id");
        }
    }
}
