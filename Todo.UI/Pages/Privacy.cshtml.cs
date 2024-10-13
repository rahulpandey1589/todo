using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Todo.UI.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                int a = 10;
                int b = 0;


                _logger.LogInformation($"The first number entered by end user is {a}");
                _logger.LogInformation($"The second number entered by end user is {b}");



                if(b == 0)
                {
                    _logger.LogInformation("Divide by 0 is not allowed");
                    return;
                }

                int c = a / b;
            }
            catch (Exception ex)
            {
               _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
