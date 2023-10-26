using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/[controller]/[action]")]
public class MailController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Send()
    {
        return Ok(await Task.Run(() => "Mail Sent"));
    }
}
