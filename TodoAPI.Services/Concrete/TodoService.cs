using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;
using TodoAPI.Models;
using TodoAPI.Services.Interfaces;
using TodoAPI.Services.Mappers;

namespace TodoAPI.Services.Concrete
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public bool DeleteNonLinkedRecords(int id)
        {
            return _todoRepository.DeleteNonLinkedRecords(id);  
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
           return await _todoRepository.DeleteTodoAsync(id);
        }

        public  bool FetchTodoByProcedure(int todoId)
        {
           return  _todoRepository.FetchTodoByProcedure(todoId);   
        }

        public IEnumerable<TodoModel> GetAllTodos()
        {
            // depedency Injection


            List<TodoModel> todoModelList = new List<TodoModel>();

            var todos = _todoRepository.GetAll();

            return todos.ConvertToTodoModel();
        }

        public IEnumerable<TodoModel> GetAllTodos(bool fetchPendingOnly)
        {
            var todoList = _todoRepository.GetAll(fetchPendingOnly);
            return todoList.ConvertToTodoModel();
        }

        public bool InsertNewTodo(TodoModel todoModel)
        {
            var todo = todoModel.ConvertToTodoList(); // extension method

            return _todoRepository.InsertTodo(todo);
        }

        public bool UpdateTodo(int  id)
        {
            // write own logic here

           return _todoRepository.UpdateTodo(id);
        }

        public bool UpdateTodo(int Id, string taskName)
        {
           return _todoRepository.UpdateTodo(Id, taskName);
        }
    }


 
}
