using Todo.Persistence.Domain;

namespace Todo.Persistence.Interfaces
{
    public interface ITodoRepository
    {
        IQueryable<TodoList> GetAll(); // get all the Todo's in database

        bool InsertTodo(TodoList todo);

        IQueryable<TodoList> GetAll(bool fetchPending);

        Task<bool> DeleteTodoAsync(int id);

        bool UpdateTodo(int  Id);

        bool UpdateTodo(int id, string taskName);

        bool DeleteNonLinkedRecords(int id);


    }
}
