using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Interfaces
{
    public interface ITodoRepository
    {
        IQueryable<TodoList> GetAll(); // get all the Todo's in database

        bool InsertTodo(TodoList todo);

        IQueryable<TodoList> GetAll(bool fetchPending);

    }
}
