using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VersionResponse))]
    public IActionResult GetVersion() =>
        new OkObjectResult(
            new VersionResponse
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            }
        );
}

[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public record class VersionResponse
{
    public string? Version { get; init; }
}
