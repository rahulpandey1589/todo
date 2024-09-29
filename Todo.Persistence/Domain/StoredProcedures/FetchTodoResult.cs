using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Persistence.Domain.StoredProcedures
{
    public class FetchTodoResult
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; } = default!;
        public bool IsCompleted { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string TaskName { get; set; } = default!;
        public string TaskType { get; set; } = default!;

    }
}
