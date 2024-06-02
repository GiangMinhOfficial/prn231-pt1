using DemoMediaFormatters.Formatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Xml.Serialization;
using WebApiContrib.Core.Formatter.Bson;

namespace DemoMediaFormatters {

    public class Program {

        public static void Main(string[] args) {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => {
                //options.OutputFormatters.Add(new CsvOutputFormatter());
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                //options.InputFormatters.RemoveType<SystemTextJsonInputFormatter>();
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.InputFormatters.Add(new CsvInputFormatter());
                options.OutputFormatters.Add(new CsvOutputFormatter());
            }
            );
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.AllowSynchronousIO = true;
            });
            //builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //    builder.Services.AddControllers().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            builder.Services.AddMvc().AddBsonSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen();

            // Create a new instance of HttpConfiguration


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}