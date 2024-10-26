using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using Todo.UI.Models;

namespace Todo.UI.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl; 
        public List<CreateTodoRequestModel> Todos { get; set; }



        public IndexModel(
            ILogger<IndexModel> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("TodoApiUrl")!;
        }

        public async Task OnGetAsync()
        {
           await ListTodos();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string deleteUrl = _baseUrl + $"/Todo?id={id}";

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

        public async Task ListTodos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(_baseUrl + "/Todo");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Todos = JsonConvert.DeserializeObject<List<CreateTodoRequestModel>>(responseBody)!;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
