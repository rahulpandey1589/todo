using TodoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services.Concrete;
using TodoAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;

namespace Todo.API.Controllers
{
    // Separation of concerns
    // Route

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

            return Ok(allTodos);
        }

        [HttpGet]
        [Route("fetchActiveTodos")]
        public async Task<IActionResult> Get(bool fetchActiveTodos)
        {
            var allTodos = _todoService.GetAllTodos(fetchActiveTodos);

            return Ok(allTodos);
        }


        [HttpPost] // insert records into database
        [Route("post")]
        public async Task<IActionResult> Post([FromBody]TodoModel todoModel)
        {
            var response = _todoService.InsertNewTodo(todoModel);
            return Ok(response);
        }

        [HttpPut] // update records in database
        public async Task<IActionResult> Put()
        {
            return Ok();
        }


        [HttpDelete] // delete a record from database
        public async Task<IActionResult> Delete(int id)
        {

            var response = _todoService.DeleteTodo(id);

            return Ok(response);
        }
    }
}
