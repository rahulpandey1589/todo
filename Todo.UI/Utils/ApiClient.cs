using System.Text;

namespace Todo.UI.Utils
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            // Initialize HttpClient
            _httpClient = new HttpClient();
        }

        // GET request method
        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Throw an exception if the request failed
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error in GET request: {ex.Message}");
                return null;
            }
        }

        // POST request method
        public async Task<string> PostAsync(string url, string jsonContent)
        {
            try
            {
                // Set the content type to application/json
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                // Throw an exception if the request failed
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error in POST request: {ex.Message}");
                return null;
            }
        }
    }
}