using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;

namespace Todo.UI.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        private const string baseApiUrl = "https://localhost:7037/api/Todo";

        public List<TodoResponse> ResponseModel { get; set; }



        public IndexModel(

            ILogger<IndexModel> logger)
        {

            _logger = logger;
        }

        public void OnGet()
        {
            BindData();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string deleteUrl = baseApiUrl + $"?id={id}";

                HttpResponseMessage response = await client.DeleteAsync(deleteUrl);
                response.EnsureSuccessStatusCode();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostEdit(int id)
        {
            return RedirectToPage("/EditTodo");
        }

        public IActionResult OnPostCreateNew()
        {
            return RedirectToPage("/Create");
        }

        public void BindData()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = client.GetAsync(baseApiUrl).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    ResponseModel = JsonConvert.DeserializeObject<List<TodoResponse>>(responseBody)!;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class TodoResponse
    {
        public int Id { get; set; }

        public string TaskName { get; set; } = default!;

        public string AssignedTo { get; set; } = default!;

        public bool IsCompleted { get; set; }
        public string Description { get; set; } = default!;

        public string TaskType { get; set; } = default!;
    }
}
