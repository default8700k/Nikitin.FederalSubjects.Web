using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Reflection;

namespace Nikitin.FederalSubjects.Web.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseVersion))]
    public IActionResult GetVersion() =>
        new OkObjectResult(
            new ResponseVersion
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            }
        );
}

public record class ResponseVersion
{
    public string? Version { get; init; }
}
