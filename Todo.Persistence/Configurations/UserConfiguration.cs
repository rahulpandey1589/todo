using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Configurations
{

    /*
     *  Primary Key 
     *  Foreign key
     *  
     *  proper column with proper data type
     *  
     * 
     */


    // Fluent API 
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(
            EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(x => x.UserId);// primary key

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired();
            builder.Property(x => x.EmailAddress).HasMaxLength(256).IsRequired();
        }
    }
}
