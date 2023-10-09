using Pb.Rate.Service.Loaders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRatePlansLoader>(new RatePlansLoader(builder.Configuration["DATA:INVENTORY"]));

var app = builder.Build();

app.Run();