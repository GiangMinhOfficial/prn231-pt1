using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
namespace DemoResponseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(
                options =>
                {
                    options.ReturnHttpNotAcceptable = false;
                    //options.RespectBrowserAcceptHeader = true;
                }).AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; })
            .AddXmlSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
