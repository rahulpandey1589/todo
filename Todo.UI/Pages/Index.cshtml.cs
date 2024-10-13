using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Todo.UI.Pages
{
    public class IndexModel : PageModel
    {
      
        private readonly ILogger<IndexModel> _logger;

        public List<TodoResponse> ResponseModel { get; set; }

        public IndexModel(
       
            ILogger<IndexModel> logger)
        {
  
            _logger = logger;
        }

        public void OnGet()
        {
            string apiUrl = "https://localhost:7037/api/Todo";


            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
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
