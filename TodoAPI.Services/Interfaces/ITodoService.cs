using TodoAPI.Models;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {

        IEnumerable<TodoModel> GetAllTodos();

        bool InsertNewTodo(TodoModel todoModel);

        IEnumerable<TodoModel> GetAllTodos(bool fetchPendingOnly);

        bool DeleteTodo(int id);

    }
}
