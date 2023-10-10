using Pb.Profile.Service.Models;
using Pb.Profile.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IProfileService, ProfileService>();
builder.Services.AddSingleton<IHotelLoader>(new HotelLoader(builder.Configuration["DATA:HOTELS"]));

var app = builder.Build();

app.MapControllers();

app.Run();