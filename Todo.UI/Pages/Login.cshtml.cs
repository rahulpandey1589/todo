using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Todo.UI.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;

        public LoginModel(
            IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration!.GetValue<string>("TodoApiUrl")!;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
            using (HttpClient client = new HttpClient())
            {
                string loginUrl = _baseUrl + $"/Authentication?userName={Username}&password={Password}";

                HttpResponseMessage response = await client.GetAsync(loginUrl);
                response.EnsureSuccessStatusCode();

                string tokenString = await response.Content.ReadAsStringAsync();

                HttpContext.Session.SetString("Token", tokenString);

                Response.Cookies.Append("AuthToken", tokenString, new CookieOptions { HttpOnly = true });

                return RedirectToPage("Index");
            }
        }
    }
}
