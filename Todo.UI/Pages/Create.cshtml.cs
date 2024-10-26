using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Todo.UI.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Task Name is required")]
        [MinLength(3, ErrorMessage = "Task Name should be atleast 3 character long.")]
        [MaxLength(15, ErrorMessage = "Task Name should not be more than 15 character long.")]
        public string NewTask { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage = "Assigned to is required")]
        public string SelectedAssignedTo { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage = "Description to is required")]
        public string Description { get; set; } = default!;

        private const string baseApiUrl = "https://localhost:7037/api";


        public List<SelectListItem> AssignedTo { get; set; }

        public IActionResult OnGet()
        {
            BindAssignedTo();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                TodoResponse todoData = new TodoResponse()
                {
                    TaskName = NewTask,
                    AssignedTo = SelectedAssignedTo,
                    IsCompleted = false,
                    Description = Description,
                    TaskType = "urgent"
                };
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync<TodoResponse>(baseApiUrl + "/Todo/post", todoData);
                    response.EnsureSuccessStatusCode();

                    return RedirectToPage("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void BindAssignedTo()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = client.GetAsync(baseApiUrl + "/User").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var responseModel = JsonConvert.DeserializeObject<List<UserResponse>>(responseBody)!;

                    if (responseModel.Count > 0)
                    {
                        var assigned = responseModel.Select(x => new SelectListItem
                        {
                            Text = x.EmailAddress,
                            Value = x.UserName
                        }).ToList();

                        AssignedTo = assigned;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    public class UserResponse
    {

        public string UserName { get; set; } = default!;

        public string EmailAddress { get; set; } = default!;
    }
}
