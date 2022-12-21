using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;
[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    // Esse é o Health-Check do sistema!
    [HttpGet("")]
    public IActionResult get()
    {
        return Ok(new
        {
            systemIsWorking = "Sistema esta funcionando"
        });
    }
}