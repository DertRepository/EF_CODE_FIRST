namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationToCourseTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String());
        }
    }
}
