using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(
            EntityTypeBuilder<TodoList> builder)
        {
            builder.HasKey(x => x.Id); // primary key 

            builder.Property(x => x.TaskName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsCompleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.AssignedTo).HasMaxLength(100);


            builder.HasOne<TodoDetails>(s => s.Details)
                .WithOne(ad => ad.TodoList)
                .HasForeignKey<TodoDetails>(ad => ad.TodoId);
        }
    }
}
