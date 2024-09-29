using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Todo.Persistence.Domain
{

    public class TodoList : AuditEntity
    {
        public int Id { get; set; }

        public string TaskName { get; set; } = default!;

        public string AssignedTo { get; set; } = default!;

        public bool IsCompleted { get; set; }

        public TodoDetails Details { get; set; }    
    }
}
