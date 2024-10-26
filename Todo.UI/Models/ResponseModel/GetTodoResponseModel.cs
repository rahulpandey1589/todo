namespace Todo.UI.Models.ResponseModel
{
    public class GetTodoResponseModel
    {
        public int id { get; set; }
        public string assignedTo { get; set; }
        public bool isCompleted { get; set; }
        public string description { get; set; }
        public string taskName { get; set; }
        public string taskType { get; set; }
    }
}
