namespace WebExamenator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Password = c.Int(nullable: false),
                        Procent3 = c.Int(nullable: false),
                        Procent4 = c.Int(nullable: false),
                        Procent5 = c.Int(nullable: false),
                        AmountTask = c.Int(nullable: false),
                        TimeExamen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TextTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdExamen = c.Int(nullable: false),
                        Title = c.String(),
                        Question = c.String(),
                        Answer1 = c.String(),
                        CorrectAns1 = c.Boolean(nullable: false),
                        Answer2 = c.String(),
                        CorrectAns2 = c.Boolean(nullable: false),
                        Answer3 = c.String(),
                        CorrectAns3 = c.Boolean(nullable: false),
                        Answer4 = c.String(),
                        CorrectAns4 = c.Boolean(nullable: false),
                        Examen_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examen", t => t.Examen_Id)
                .Index(t => t.Examen_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextTasks", "Examen_Id", "dbo.Examen");
            DropIndex("dbo.TextTasks", new[] { "Examen_Id" });
            DropTable("dbo.TextTasks");
            DropTable("dbo.Examen");
        }
    }
}
