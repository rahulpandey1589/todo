using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Todo.UI.Models.ViewModel
{
    public class CreateTodoViewModel
    {
        [Required(ErrorMessage = "Task Name is required")]
        [MinLength(3, ErrorMessage = "Task Name should be atleast 3 character long.")]
        [MaxLength(15, ErrorMessage = "Task Name should not be more than 15 character long.")]
        public string NewTask { get; set; } = default!;

        [Required(ErrorMessage = "Assigned to is required")]
        public string SelectedAssignedTo { get; set; } = default!;


        [Required(ErrorMessage = "Description to is required")]
        public string Description { get; set; } = default!;
    }
}
