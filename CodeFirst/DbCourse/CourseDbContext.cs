using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CodeFirst
{
    public partial class CourseDbContext : DbContext
    {
        public CourseDbContext()
            : base("data source=TS-1446G63;initial catalog=CourseCodeFirst;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent API
            //modelBuilder.Entity<Course>()
            //    .Property(e => e.Description)
            //    .IsRequired();

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Author)
                .HasForeignKey(e => e.Author_Id);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("TagCourses").MapLeftKey("Course_Id"));
        }
    }
}
