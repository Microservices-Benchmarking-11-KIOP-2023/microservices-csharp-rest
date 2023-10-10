using Pb.Rate.Service.Loaders;
using Pb.Rate.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IRateService, RateService>();
builder.Services.AddSingleton<IRatePlansLoader>(new RatePlansLoader(builder.Configuration["DATA:INVENTORY"]));

var app = builder.Build();

app.MapControllers();

app.Run();