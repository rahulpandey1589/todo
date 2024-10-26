using Newtonsoft.Json;
using Todo.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Todo.UI.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.UI.Models.RequestModel;


namespace Todo.UI.Pages
{
    public class EditTodoModel : PageModel
    {
        [BindProperty] 
        public EditTodoViewModel EditTodoViewModel { get; set; } = new EditTodoViewModel();
        
        [FromQuery(Name = "todoId")]
        public int TodoId { get; set; }

        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;
        public List<SelectListItem> AssignedTo { get; set; }


        public EditTodoModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("TodoApiUrl")!;
            AssignedTo = new List<SelectListItem>();
        }

        public async Task OnGetAsync(int todoId)
        {
            await PopulateAssignedToDropDown();
            await PopulateTodoDetails(todoId);
        }

        public async Task PopulateTodoDetails(int todoId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response =
                await client.GetAsync(_baseUrl + "/Todo/fetchByProcedure?todoId=" + todoId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<GetTodoResponseModel>(responseBody)!;

            EditTodoViewModel.NewTask = responseModel.taskName;
            EditTodoViewModel.Description = responseModel.description;
            EditTodoViewModel.SelectedAssignedTo =
                AssignedTo.FirstOrDefault(
                        x => x.Value == responseModel.assignedTo.ToString())!
                    .Value;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreateTodoRequestModel todoData = new()
            {
                Id = TodoId,
                TaskName = EditTodoViewModel.NewTask,
                AssignedTo = EditTodoViewModel.SelectedAssignedTo,
                IsCompleted = false,
                Description = EditTodoViewModel.Description,
                TaskType = "urgent"
            };
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync<CreateTodoRequestModel>(_baseUrl + "/Todo/post", todoData);
                response.EnsureSuccessStatusCode();

                return RedirectToPage("Index");
            }
        }
            
        
        public async Task PopulateAssignedToDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_baseUrl + "/User");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
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
    }
}