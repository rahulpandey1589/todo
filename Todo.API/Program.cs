
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Todo.API.OptionSetup;
using Todo.Persistence.Concrete;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;
using TodoAPI.Services.Concrete;
using TodoAPI.Services.Interfaces;

namespace Todo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("https://localhost:7194/")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            builder.Services.ConfigureOptions<JwtOptionSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionSetup>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            string connectionString = builder.Configuration.GetConnectionString("TodoDbContext")!;
            var signingKey = builder.Configuration.GetValue<string>("JsonWebTokenKeys:ValidateIssuerSigningKey");
            builder.Services.AddDbContext<TodoDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<ITodoService, TodoService>();
            builder.Services.AddTransient<ITodoRepository, TodoRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IKeyVaultService, KeyVaultService>();
            
            var app = builder.Build();

            
          
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
