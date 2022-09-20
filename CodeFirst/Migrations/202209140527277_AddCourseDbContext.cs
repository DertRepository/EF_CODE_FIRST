namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublishedDate = c.DateTime(),
                        Description = c.String(),
                        Level = c.Int(nullable: false),
                        FullPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Author_Id = c.Int(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagCourses",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Tag_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.TagCourses", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Category_Id", "dbo.Categories");
            DropIndex("dbo.TagCourses", new[] { "Tag_Id" });
            DropIndex("dbo.TagCourses", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "Category_Id" });
            DropIndex("dbo.Courses", new[] { "Author_Id" });
            DropTable("dbo.TagCourses");
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
            DropTable("dbo.Courses");
            DropTable("dbo.Authors");
        }
    }
}
