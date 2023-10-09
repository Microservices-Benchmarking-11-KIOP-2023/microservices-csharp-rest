using Pb.Common.Clients;

namespace Pb.ApiGateway.Setup;

public static class HttpClientsSetup
{
    public static IServiceCollection SetupHttpServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<ISearchClient, SearchClient>(o =>
        {
            o.BaseAddress = new Uri(config.GetSection("SERVICES:ADDRESSES:SEARCH").Value ??
                                    throw new InvalidOperationException());
        });
        
        services.AddHttpClient<IProfileClient, ProfileClient>(o =>
        {
            o.BaseAddress = new Uri(config.GetSection("SERVICES:ADDRESSES:PROFILE").Value ??
                                    throw new InvalidOperationException());
        });
        
        return services;
    }
}
