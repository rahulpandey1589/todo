using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence;
using Todo.Persistence.Interfaces;
using TodoAPI.Models;
using TodoAPI.Services.Interfaces;
using TodoAPI.Services.Mappers;

namespace TodoAPI.Services.Concrete
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
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
            var todo = todoModel.ConvertToTodoList();

            return _todoRepository.InsertTodo(todo);
        }
    }
}
