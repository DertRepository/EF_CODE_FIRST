using Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueriesLINQ.EntityConfigurations
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
