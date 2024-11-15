﻿namespace Todo.UI.Models.RequestModel
{
    public class CreateTodoRequestModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; } = default!;

        public string AssignedTo { get; set; } = default!;

        public bool IsCompleted { get; set; }
        public string Description { get; set; } = default!;

        public string TaskType { get; set; } = default!;
    }
}
