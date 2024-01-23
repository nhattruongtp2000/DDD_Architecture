using System.Net;
using Newtonsoft.Json;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = "An error occured " });
            context.Response.ContentType="application/json";
            context.Response.StatusCode = (int)code; 
        }
    }
}
