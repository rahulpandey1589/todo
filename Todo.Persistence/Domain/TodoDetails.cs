namespace Todo.Persistence.Domain
{
    public class TodoDetails : AuditEntity
    {
        public int Id { get; set; }
        public int TodoId { get; set; }  // foreign key 
        public string Description { get; set; } = default!;
        public string TaskType { get; set; } = default!;
        public TodoList TodoList { get; set; }
    }
}
