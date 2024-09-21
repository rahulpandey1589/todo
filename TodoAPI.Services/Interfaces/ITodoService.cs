using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {

        IEnumerable<TodoModel> GetAllTodos();

        bool InsertNewTodo(TodoModel todoModel);

        IEnumerable<TodoModel> GetAllTodos(bool fetchPendingOnly);


    }
}
