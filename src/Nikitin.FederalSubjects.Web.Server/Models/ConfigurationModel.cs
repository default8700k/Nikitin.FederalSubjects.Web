using Nikitin.FederalSubjects.Web.Server.Options;

namespace Nikitin.FederalSubjects.Web.Server.Models;

public record class ConfigurationModel
{
    public OptionsExternalService ExternalService { get; init; } = null!;
}
