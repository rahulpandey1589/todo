using Microsoft.Extensions.Options;
using TodoAPI.Models;

namespace Todo.API.OptionSetup
{
    public class JwtOptionSetup : IConfigureOptions<JwtModel>
    {

        private readonly IConfiguration _configuration;


        public JwtOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void Configure(JwtModel options)
        {
            _configuration.GetSection("JsonWebTokenKeys").Bind(options);  /// read JsonWebTokenKeys from appsettings.development.json and bind it to JwtModel
        }
    }
}
