using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Todo.UI.Models.RequestModel;

namespace Todo.UI.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseUrl;
        public List<CreateTodoRequestModel> Todos { get; set; }



        public IndexModel(
            ILogger<IndexModel> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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
            return RedirectToPage("/EditTodo", new { todoId = id });
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

                    var token = _httpContextAccessor.HttpContext!.Session.GetString("Token");

                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    HttpResponseMessage response = await client.GetAsync(_baseUrl + "/Todo");

                    var statusCode = response.StatusCode;

                    if(statusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                      
                       
                    }
                    else
                    {


                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Todos = JsonConvert.DeserializeObject<List<CreateTodoRequestModel>>(responseBody)!;
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
