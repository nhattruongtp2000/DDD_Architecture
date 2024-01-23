using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filter
{
    public class ErrorHandlingFilterAttribute :ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var excetpion = context.Exception;

            var problemDetails = new ProblemDetails
            {
                Type = "google.com",
                Title = "An error occured when ff",
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Result = new ObjectResult(new { error = "An error occured when ff" })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
