using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Configurations
{
    public class TodoDetailConfiguration : IEntityTypeConfiguration<TodoDetails>
    {
        public void Configure(
            EntityTypeBuilder<TodoDetails> builder)
        {
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}
