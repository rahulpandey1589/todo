using TodoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services.Concrete;
using TodoAPI.Services.Interfaces;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
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
        public async Task<IActionResult> Post([FromBody] TodoModel todoModel)
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
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
