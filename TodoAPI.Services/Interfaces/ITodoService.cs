using TodoAPI.Models;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {

        IEnumerable<TodoModel> GetAllTodos();

        bool InsertNewTodo(TodoModel todoModel);

        IEnumerable<TodoModel> GetAllTodos(bool fetchPendingOnly);

        Task<bool> DeleteTodoAsync(int id);

        bool UpdateTodo(int id);

        bool UpdateTodo(int Id, string taskName);

        bool DeleteNonLinkedRecords(int id);


    }
}
