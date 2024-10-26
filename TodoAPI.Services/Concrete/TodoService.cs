using System.Net.Http.Headers;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;
using TodoAPI.Models;
using TodoAPI.Models.Response;
using TodoAPI.Services.Interfaces;
using TodoAPI.Services.Mappers;

namespace TodoAPI.Services.Concrete
{
    /*
     * 1. CRUD Todo
     * 2. Create an API to find records based on AssignedTo
     * 3. Create an API to find pending Todo's
     * 4. Create an API to find todo's created in last 7 days
     * 
     * 
     * 
     * 1. Create Employee, department
     * 2. join between employee, department
     * 
     * 
     * 
     * 1. Customer
     * 2. Order Table
     * 3. Product
     */

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

        public FetchByProcedureResponseModel FetchTodoByProcedure(int todoId)
        {

            //var output = _todoRepository.FetchDataUsingLinqJoin(todoId, useLamdaOperator: true);

            //var ouput2 = _todoRepository.OrderByExample(false);


            var dataResponse = _todoRepository.FetchTodoByProcedure(todoId);

            if (dataResponse is not null)
            {
                return new FetchByProcedureResponseModel()
                {
                    AssignedTo = dataResponse.AssignedTo,
                    Description = dataResponse.Description,
                    Id = dataResponse.Id,
                    IsCompleted = true,
                    TaskName = dataResponse.TaskName,
                    TaskType = dataResponse.TaskType
                };
            }
            return new FetchByProcedureResponseModel();
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

        public bool UpdateTodo(int id)
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
