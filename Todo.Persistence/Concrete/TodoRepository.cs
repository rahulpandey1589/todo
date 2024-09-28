using Microsoft.EntityFrameworkCore;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;


/*
 *   Stage
 *   Commit
 *   Push
 */

namespace Todo.Persistence.Concrete
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public bool DeleteNonLinkedRecords(int id)
        {
            var existingItem =
                  _context.Todos.Include(x => x.Details)
                         .Where(x => x.Id == id)
                      .FirstOrDefault(); // fetch data based on ID itself


            var sampleData = _context.SampleTables.Find(1);

            _context.Todos.Remove(existingItem!);  // telling EF to delete this record

            _context.SampleTables.Remove(sampleData);

            int recordDeleted = _context.SaveChanges();

            return recordDeleted > 0 ? true : false;  // ternary operator
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var existingItem =
                await _context.Todos.Include(x => x.Details)
                        .Where(x => x.Id == id)
                     .FirstOrDefaultAsync(); // fetch data based on ID itself
                                             // 
            _context.Todos.Remove(existingItem!);  // telling EF to delete this record

            _context.Entry(existingItem).State = EntityState.Deleted;

            int recordDeleted = await _context.SaveChangesAsync();

            return recordDeleted > 0 ? true : false;  // ternary operator
        }

        public IQueryable<TodoList> GetAll()
        {
            return _context.Todos.Include(x => x.Details);
        }

        public IQueryable<TodoList> GetAll(bool fetchPending)
        {
            return _context.Todos.Where(x => x.IsCompleted == fetchPending);  // LINQ --Lamda operator
        }

        public bool InsertTodo(TodoList todo)
        {
            _context.Todos.Add(todo);

            int recordsInserted = _context.SaveChanges();

            return recordsInserted > 0 ? true : false;  // ternary operator
        }

        public bool UpdateTodo(int id)
        {
            var existingTodo = _context.Todos.Find(id);

            if (existingTodo != null)
            {
                existingTodo.IsCompleted = true;

                _context.Entry(existingTodo).State = EntityState.Modified;

                return _context.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public bool UpdateTodo(int id, string taskName)
        {
            var existingTodo = _context.Todos.Find(id);

            if (existingTodo != null)
            {
                existingTodo.TaskName = taskName;

                _context.Entry(existingTodo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                return _context.SaveChanges() > 0 ? true : false;
            }
            return false;

        }
    }
}
