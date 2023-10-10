using Pb.Search.Service.Services;
using Pb.Search.Service.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.SetupHttpServices(builder.Configuration);
builder.Services.AddSingleton<ISearchService, SearchService>();

var app = builder.Build();

app.MapControllers();

app.Run();