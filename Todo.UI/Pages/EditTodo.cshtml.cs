using Newtonsoft.Json;
using Todo.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Todo.UI.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Todo.UI.Pages
{
    public class EditTodoModel : PageModel
    {
        [BindProperty]
        public EditTodoViewModel EditTodoViewModel { get; set; } = new EditTodoViewModel();

        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;
        public List<SelectListItem> AssignedTo { get; set; }


        public EditTodoModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("TodoApiUrl")!;
            AssignedTo = new List<SelectListItem>();
        }

        public async Task OnGet(int todoId)
        {
            await PopulateAssignedToDropDown();
            await PopulateData(todoId);
        }

        public async Task PopulateData(int todoId)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(_baseUrl + "/Todo/fetchByProcedure?todoId=" + todoId);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseModel = JsonConvert.DeserializeObject<GetTodoResponseModel>(responseBody)!;

                    if (responseModel != null)
                    {
                        EditTodoViewModel.NewTask = responseModel.taskName;
                        EditTodoViewModel.Description = responseModel.description;
                        EditTodoViewModel.SelectedAssignedTo =
                            AssignedTo.Where(x => x.Value == responseModel.assignedTo.ToString()).FirstOrDefault()!.Value;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task PopulateAssignedToDropDown()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
