using Pb.Profile.Service.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHotelLoader>(new HotelLoader(builder.Configuration["DATA:HOTELS"]));
var app = builder.Build();

app.Run();