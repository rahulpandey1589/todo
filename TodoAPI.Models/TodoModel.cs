namespace TodoAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; } = default!;

        public string AssignedTo { get; set; } = default!;

        public bool IsCompleted { get; set; }
        public string Description { get; set; } = default!;

        public string TaskType { get; set; } = default!;
    }


}
