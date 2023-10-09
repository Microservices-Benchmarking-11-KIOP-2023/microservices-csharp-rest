using Pb.Geo.Service.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IPointLoader>(new PointLoader(builder.Configuration["DATA:GEO"]));
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
