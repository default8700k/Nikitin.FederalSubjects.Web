using Microsoft.AspNetCore.Mvc.Testing;

namespace Nikitin.FederalSubjects.Web.Server.Tests.Factories;

public class WebServiceDefaultFactory : WebApplicationFactory<Startup>
{
    public HttpClient HttpClient { get; }

    public WebServiceDefaultFactory()
    {
        HttpClient = base.CreateClient();
    }
}
