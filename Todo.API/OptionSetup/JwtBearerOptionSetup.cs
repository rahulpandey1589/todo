using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoAPI.Models;

namespace Todo.API.OptionSetup
{
    public class JwtBearerOptionSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtModel _jwtOptions;

        public JwtBearerOptionSetup(IOptions<JwtModel> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = _jwtOptions.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey)),
                ValidateIssuer = _jwtOptions.ValidateIssuer,
                ValidateAudience = _jwtOptions.ValidateAudience,
                RequireExpirationTime = true,
                ValidateLifetime = _jwtOptions.ValidateLifeTime,
              
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(Options.DefaultName, options);
        }
    }
}
