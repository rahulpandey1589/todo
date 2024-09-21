using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Concrete
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }


        public IQueryable<TodoList> GetAll()
        {
            return _context.Todos;
        }

        public IQueryable<TodoList> GetAll(bool fetchPending)
        {
           return _context.Todos.Where(x => x.IsCompleted == fetchPending);  // LINQ --Lamda operator
        }

        public bool InsertTodo(TodoList todo)
        {
            todo.CreatedBy = "System";
            _context.Todos.Add(todo);

            int recordsInserted = _context.SaveChanges();

            return recordsInserted > 0 ? true:false;  // ternary operator
        }
    }
}
