using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Todo.UI.Models.RequestModel;
using Todo.UI.Models.ResponseModel;
using Todo.UI.Models.ViewModel;

namespace Todo.UI.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateTodoViewModel CreateTodoModel { get; set; } = new CreateTodoViewModel();
        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;

        public List<SelectListItem> AssignedTo { get; set; } = new List<SelectListItem>();

        public CreateModel(IConfiguration configuration)
        {

            this._configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("TodoApiUrl")!;
        }

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

                CreateTodoRequestModel todoData = new()
                {
                    TaskName = CreateTodoModel.NewTask,
                    AssignedTo = CreateTodoModel.SelectedAssignedTo,
                    IsCompleted = false,
                    Description = CreateTodoModel.Description,
                    TaskType = "urgent"
                };
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync<CreateTodoRequestModel>(_baseUrl + "/Todo/post", todoData);
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

                    HttpResponseMessage response = client.GetAsync(_baseUrl + "/User").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var responseModel = JsonConvert.DeserializeObject<List<UserResponseModel>>(responseBody)!;

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



}
