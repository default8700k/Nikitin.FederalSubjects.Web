namespace Nikitin.FederalSubjects.Web.Server.Options;

public record class OptionsExternalService
{
    public string BaseAddress { get; init; } = null!;
    public TimeSpan Timeout { get; init; }
}
