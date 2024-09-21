namespace Todo.Persistence.Domain
{
    public class AuditEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; } = default!;
    }
}
