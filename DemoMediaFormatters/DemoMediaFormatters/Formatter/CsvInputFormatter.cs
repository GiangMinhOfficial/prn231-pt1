using DemoMediaFormatters.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace DemoMediaFormatters.Formatter
{
    public class CsvInputFormatter : TextInputFormatter
    {
        public CsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(Product);
        }

        public async override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            using var reader = new StreamReader(httpContext.Request.Body, encoding);
            string? dataLine = null;
            try
            {
                //await ReadLineAsync($"Id,Name,Category,Price", reader, context);
                dataLine = await ReadLineAsync(null, reader, context);
                var data = dataLine.Split(',');
                var product = new Product()
                {
                    ProductId = Convert.ToInt32(data[0]),
                    ProductName = data[1],
                    ProductCategory = data[2],
                    ProductPrice = Convert.ToDecimal(data[3]),
                };

                return await InputFormatterResult.SuccessAsync(product);

            }
            catch (Exception)
            {
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(
     string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();

            if (expectedText != null)
            {
                if (line is null || !line.StartsWith(expectedText))
                {
                    var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                    context.ModelState.TryAddModelError(context.ModelName, errorMessage);


                    throw new Exception(errorMessage);
                }
            }
     

            return line;
        }
    }
}
