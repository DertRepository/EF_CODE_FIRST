using System.Data.Entity;
using Queries.EntityConfigurations;
using QueriesLINQ.EntityConfigurations;

namespace Queries
{
    public class CourseContext : DbContext
    {
        public CourseContext()
            : base("name=CourseContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());
            modelBuilder.Configurations.Add(new AuthorConfiguration());
        }
    }
}
