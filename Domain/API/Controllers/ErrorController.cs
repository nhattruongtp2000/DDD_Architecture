using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Index()
        {
            return Problem();
        }
    }
}
