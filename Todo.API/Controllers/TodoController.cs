using TodoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace Todo.API.Controllers
{
    // Separation of concerns
    // Route

    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)  // constructor injection
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //using(SqlConnection connection = new SqlConnection())
            //{
            //    connection.ConnectionString = "Data Source=.; Initial Catalog=TodoDatabase; Integrated Security=True;TrustServerCertificate=True;";
            //    connection.Open();


            //    SqlCommand cmd = new SqlCommand("Select * from Todos", connection);
            //    SqlDataReader sqlDataReader = cmd.ExecuteReader();

            //    while (sqlDataReader.Read())
            //    {
            //        string taskName = sqlDataReader["TaskName"].ToString();
            //    }

            //}

            var allTodos = _todoService.GetAllTodos();

            return Ok(await  Task.FromResult(allTodos));
        }

        [HttpGet]
        [Route("fetchActiveTodos")]
        public IActionResult Get(bool fetchActiveTodos)
        {
            var allTodos = _todoService.GetAllTodos(fetchActiveTodos);

            return Ok(allTodos);
        }


        [HttpGet]
        [Route("fetchByProcedure")]
        public IActionResult FetchByProcedure(int todoId)
        {
            var allTodos = _todoService.FetchTodoByProcedure(todoId);

            return Ok(allTodos);
        }


        [HttpPost] // insert records into database
        [Route("post")]
        public async Task<IActionResult> Post([FromBody] TodoModel todoModel)
        {
            var response = _todoService.InsertNewTodo(todoModel);
            return Ok(response);
        }

        [HttpPut] // update records in database
        [Route("completeTodo")]
        public IActionResult CompleteTodo(int todoId)
        {
            var response = _todoService.UpdateTodo(todoId);
            return Ok(response);
        }

        [HttpPut] // update records in database
        [Route("updateTodo")]
        public IActionResult UpdateTodoItem(int todoId, string taskName)
        {
            var response = _todoService.UpdateTodo(todoId, taskName);
            return Ok(response);
        }


        [HttpDelete] // delete a record from database
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _todoService.DeleteTodoAsync(id);

            return Ok(response);
        }

        [HttpDelete] // delete a record from database
        [Route("deleteNonLinkedRecords")]
        public  IActionResult DeleteNonLinkedRecords(int id)
        {
            var response = _todoService.DeleteNonLinkedRecords(id);

            return Ok(response);
        }
    }
}
