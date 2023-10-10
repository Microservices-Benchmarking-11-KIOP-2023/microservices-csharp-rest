using Pb.Common.Clients;

namespace Pb.Search.Service.Setup;

public static class HttpClientsSetup
{
    public static IServiceCollection SetupHttpServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<IRateClient, RateClient>(o =>
        {
            o.BaseAddress = new Uri(config.GetSection("SERVICES:ADDRESSES:RATE").Value ??
                                    throw new InvalidOperationException());
        });
        
        services.AddHttpClient<IGeoClient, GeoClient>(o =>
        {
            o.BaseAddress = new Uri(config.GetSection("SERVICES:ADDRESSES:GEO").Value ??
                                    throw new InvalidOperationException());
        });
        
        return services;
    }
}