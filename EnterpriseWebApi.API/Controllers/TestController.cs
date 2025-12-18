using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWebApi.API.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("exception")]
    public IActionResult Throw()
    {
        throw new Exception("This is a test exception");
    }
}
