using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace Todo.UI.Pages
{
    public class IndexModel : PageModel
    {
      
        private readonly ILogger<IndexModel> _logger;
        private const string baseApiUrl = "https://localhost:7037/api/Todo";

        public List<TodoResponse> ResponseModel { get; set; }

        [BindProperty]
        [Required(ErrorMessage ="Task Name is required")]
        public string NewTask { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Assigned to is required")]
        public string AssignedTo { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description to is required")]
        public string Description { get; set; }

        public IndexModel(
       
            ILogger<IndexModel> logger)
        {
  
            _logger = logger;
        }

        public void OnGet()
        {
            BindData();
        }


        public void OnPost()
        {
            if (!string.IsNullOrEmpty(NewTask))
            {

                try
                {
                    TodoResponse todoData = new TodoResponse()
                    {
                        TaskName = NewTask,
                        AssignedTo = AssignedTo,
                        IsCompleted = false,
                        Description = Description,
                        TaskType = "urgent"
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = client.PostAsJsonAsync<TodoResponse>(baseApiUrl + "/post", todoData).Result;
                        response.EnsureSuccessStatusCode();
                    }

                    BindData();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        public void BindData()
        {
           // string apiUrl = "https://localhost:7037/api/Todo";


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
