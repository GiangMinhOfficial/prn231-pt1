using DemoMediaFormatters.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace DemoMediaFormatters.Formatter {
    public class CsvOutputFormatter :TextOutputFormatter {
        public CsvOutputFormatter() {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type) {
            if (type == typeof(Product)) {
                return true;
            } else {
                Type enumerableType = typeof(IEnumerable<Product>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context,Encoding selectedEncoding) {
            var httpContext = context.HttpContext;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Product> products) {
                foreach (var product in products) {
                    FormatCSV(buffer,product);
                }
            } else {
                FormatCSV(buffer,(Product)context.Object!);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(),selectedEncoding);
        }

        private void FormatCSV(StringBuilder buffer,Product contact) {
            buffer.Append($"{contact.ProductId};");
            buffer.Append($"{contact.ProductName};");
            buffer.Append($"{contact.ProductCategory};");
            buffer.Append($"{contact.ProductPrice}");
            buffer.AppendLine();

        }
    }
}
