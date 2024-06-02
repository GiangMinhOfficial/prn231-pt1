using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace ContentNegotiationDemo.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/xml");
            SupportedMediaTypes.Add("application/rdf+xml");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            return type != null && type.IsSerializable || base.CanWriteType(type);

            //return typeof(Blog).IsAssignableFrom(type) || typeof(IEnumerable<Blog>).IsAssignableFrom(type);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            //var acceptType = context.HttpContext.
            var acceptHeader = context.HttpContext.Request.Headers["Accept"].ToString();

            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Blog> blogs)
            {
                foreach (var blog in blogs)
                {
                    switch (acceptHeader)
                    {
                        case "application/json":
                            FormatJson(buffer, blog);
                            break;
                        case "application/xml":
                            FormatXml(buffer, blog);
                            break;
                        default:
                            FormatJson(buffer, blog);
                            break;
                    }

                }
            }
            else
            {
                switch (acceptHeader)
                {
                    case "application/json":
                        FormatJson(buffer, (Blog)context.Object);
                        break;
                    case "application/xml":
                        FormatXml(buffer, (Blog)context.Object);
                        break;
                    default:
                        break;
                }
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);

            #region XML
            //var response = context.HttpContext.Response;
            //var xmlSerializer = new XmlSerializer(context.ObjectType);
            //using var stringWriter = new StringWriter();
            //xmlSerializer.Serialize(stringWriter, context.Object);
            //var xmlOutput = stringWriter.ToString();
            //var buffer = selectedEncoding.GetBytes(xmlOutput);
            //await response.Body.WriteAsync(buffer);
            #endregion
        }

        private static void FormatCsv(StringBuilder buffer, Blog blog)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendLine($"N:{blog.Name};{blog.Description}");
            buffer.AppendLine($"FN:{blog.Description} {blog.Name}");
            buffer.AppendLine($"UID:{blog.Name}");
            buffer.AppendLine("END:VCARD");
        }

        private static void FormatXml(StringBuilder buffer, Blog blog)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Blog));
            using (StringWriter stringWriter = new StringWriter(buffer))
            {
                serializer.Serialize(stringWriter, blog);
            }
        }

        private static void FormatJson(StringBuilder buffer, Blog blog)
        {
            string jsonString = JsonSerializer.Serialize(blog, new JsonSerializerOptions { WriteIndented = true });
            buffer.Append(jsonString);
        }
    }

}
