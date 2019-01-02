namespace ProjectManager.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriorityToTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentTask",
                c => new
                    {
                        ParentTaskId = c.Int(nullable: false, identity: true),
                        ParentTaskTitle = c.String(),
                    })
                .PrimaryKey(t => t.ParentTaskId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        ParentTaskId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Title = c.String(),
                        Priority = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.ParentTask", t => t.ParentTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ParentTaskId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(),
                        TaskId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Tasks", t => t.TaskId)
                .Index(t => t.ProjectId)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Users", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "ParentTaskId", "dbo.ParentTask");
            DropIndex("dbo.Users", new[] { "TaskId" });
            DropIndex("dbo.Users", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "ParentTaskId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.ParentTask");
        }
    }
}
