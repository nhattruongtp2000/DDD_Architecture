using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ErrorsController : ControllerBase
    {
        [Route("/error-development")]
        [HttpPost]
        public IActionResult HandleErrorDevelopment(
            [FromServices] IHostEnvironment hostEnvironment)
        {
            return Problem();
        }

        [Route("/error")]
        [HttpPost]
        public IActionResult HandleError() =>
            Problem();
    }
}
