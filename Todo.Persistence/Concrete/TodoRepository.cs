using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Todo.Persistence.Domain;
using Todo.Persistence.Domain.StoredProcedures;
using Todo.Persistence.Interfaces;


/*
 *   Stage
 *   Commit
 *   Push
 *   
 *   
 *   Joins in EF
 *   
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

        public sp_FetchTodoResult FetchTodoByProcedure(int todoId)
        {
            var response = _context.FetchTodoResult
                                .FromSqlRaw(
                                        "exec sp_FetchTodo @TodoId ={0}", todoId)
                               .ToList(); // this is possible

            return response.FirstOrDefault()!;
        }

        public IQueryable<TodoList> GetAll()
        {
            return _context.Todos.Include(x => x.Details).Where(x => x.Details != null);
        }

        public IQueryable<TodoList> GetAll(bool fetchPending)
        {
            return _context.Todos.Where(x => x.IsCompleted == fetchPending);  // LINQ --Lamda operator
        }

        public bool InsertTodo(TodoList todo)
        {
            if (todo.Id > 0)
            {
                var todoDetails
                    = _context.Todos.Where(x => x.Id == todo.Id).FirstOrDefault();

                if (todoDetails != null)
                {
                    todoDetails.TaskName = todo.TaskName;
                    todoDetails.AssignedTo = todo.AssignedTo;
                    todoDetails.IsCompleted = todo.IsCompleted;
                    todoDetails.Details = new TodoDetails()
                    {
                        TaskType = todo.Details.TaskType,
                        Description = todo.Details.Description
                    };

                    _context.Entry(todoDetails).State = EntityState.Modified;
                }
            }
            else
            {
                _context.Todos.Add(todo);
            }
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

        /*
         * 1. using Lamda operators
         * 2. using joins
         */

        public FetchTodoResult FetchDataUsingLinqJoin(int todoId, bool useLamdaOperator)
        {
            FetchTodoResult result = new();

            if (useLamdaOperator)
            {
                result = _context.Todos
                              .Join(_context.TodoDetails,
                                      td => td.Id,                     // join 
                                      tDetails => tDetails.TodoId,    // join 
                                      (td, tDetails) =>
                                       new FetchTodoResult        // Projection
                                       {
                                           AssignedTo = td.AssignedTo,
                                           Description = tDetails.Description,
                                           Id = td.Id,
                                           IsCompleted = td.IsCompleted,
                                           TaskName = td.TaskName,
                                           TaskType = tDetails.TaskType
                                       })
                              .Where(r => r.Id == todoId)
                              .FirstOrDefault()!;
            }
            else
            {


                result = (from td in _context.Todos
                          join tDetails in _context.TodoDetails
                          on td.Id equals tDetails.TodoId  // join condition
                          where td.Id == todoId // filter condition
                          select new FetchTodoResult // Projection
                          {
                              AssignedTo = td.AssignedTo,
                              Description = tDetails.Description,
                              Id = td.Id,
                              IsCompleted = td.IsCompleted,
                              TaskName = td.TaskName,
                              TaskType = tDetails.TaskType
                          }).FirstOrDefault()!;



            }

            return result;
        }

        public List<FetchTodoResult> OrderByExample(bool useLamdaOperator)
        {
            List<FetchTodoResult> result = new List<FetchTodoResult>();

            if (useLamdaOperator)
            {
                result = _context.Todos
                              .Join(_context.TodoDetails,
                                      td => td.Id,                     // join 
                                      tDetails => tDetails.TodoId,    // join 
                                      (td, tDetails) =>
                                       new FetchTodoResult        // Projection
                                       {
                                           AssignedTo = td.AssignedTo,
                                           Description = tDetails.Description,
                                           Id = td.Id,
                                           IsCompleted = td.IsCompleted,
                                           TaskName = td.TaskName,
                                           TaskType = tDetails.TaskType
                                       })
                              .OrderByDescending(x => x.TaskName).ToList();
            }
            else
            {


                result = (from td in _context.Todos
                          join tDetails in _context.TodoDetails
                          on td.Id equals tDetails.TodoId  // join condition
                          select new FetchTodoResult // Projection
                          {
                              AssignedTo = td.AssignedTo,
                              Description = tDetails.Description,
                              Id = td.Id,
                              IsCompleted = td.IsCompleted,
                              TaskName = td.TaskName,
                              TaskType = tDetails.TaskType
                          })
                          .OrderBy(x => x.TaskName)
                          .ToList();


            }

            return result;
        }
    }
}