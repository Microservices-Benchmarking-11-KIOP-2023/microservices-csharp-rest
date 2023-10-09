using Pb.ApiGateway.Providers;
using Pb.ApiGateway.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupHttpServices(builder.Configuration);
builder.Services.AddSingleton<IHotelProvider, HotelProvider>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.MapControllers();

app.Run();
