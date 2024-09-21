
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("TodoDbContext")!;


            builder.Services.AddDbContext<TodoDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<ITodoService, TodoService>();
            builder.Services.AddTransient<ITodoRepository, TodoRepository>();




            //builder.Services.AddScoped<ITodoService, TodoService>();

            //builder.Services.AddSingleton<ITodoService, TodoService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
