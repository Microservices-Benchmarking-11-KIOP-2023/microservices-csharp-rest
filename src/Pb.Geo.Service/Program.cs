using Pb.Geo.Service.Models;
using Pb.Geo.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IGeoService, GeoService>();
builder.Services.AddSingleton<IPointLoader>(new PointLoader(builder.Configuration["DATA:GEO"]));

var app = builder.Build();

app.MapControllers();

app.Run();
