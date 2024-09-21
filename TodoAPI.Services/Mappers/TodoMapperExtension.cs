﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence;
using TodoAPI.Models;

namespace TodoAPI.Services.Mappers
{
    public static class TodoMapperExtension
    {
        public static IEnumerable<TodoModel> ConvertToTodoModel(
            this IQueryable<TodoList> todoList)
        {
            List<TodoModel> todos = new List<TodoModel>();

            foreach (var todo in todoList)
            {
                TodoModel model = new TodoModel();
                model.AssignedTo = todo.AssignedTo;
                model.TaskName = todo.TaskName;
                model.IsCompleted = todo.IsCompleted;

                todos.Add(model);
            }
            return todos;
        }


        public static TodoList ConvertToTodoList(this TodoModel model)
        {
            return new TodoList()
            {
                AssignedTo = model.AssignedTo,
                TaskName = model.TaskName
            };
        }

    }
}
