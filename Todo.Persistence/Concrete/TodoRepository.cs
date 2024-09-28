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

        public bool DeleteTodo(int id)
        {
            var existingItem = _context.Todos.Where(x => x.Id == id).FirstOrDefault(); // fetch data based on ID itself
                                                                                       // 
             _context.Todos.Remove(existingItem);  // telling EF to delete this record

            _context.Entry(existingItem).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            int recordDeleted = _context.SaveChanges();

            return recordDeleted > 0 ? true : false;  // ternary operator
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

            _context.SampleTables.Add(new SampleTable()
            {
                Name = "Sample"
            });

            _context.Todos.Add(todo);

            int recordsInserted = _context.SaveChanges();

            return recordsInserted > 0 ? true:false;  // ternary operator
        }
    }
}
