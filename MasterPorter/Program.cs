//using AutoEntity.EntityModels;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// 1. Add services to the container
//builder.Services.AddControllers();

//builder.Services.AddDbContext<MasterPorterContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("EC"),
//        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EC"))
//    ));

//// Define CORS policy in service configuration
//builder.Services.AddCors(options => {
//    options.AddPolicy("AllowReactApp",
//        policy => policy.WithOrigins("http://localhost:5173")
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// 2. Build the app instance
//var app = builder.Build();

//// 3. Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// Enable CORS middleware (MUST be after app is built, before UseAuthorization)
//app.UseCors("AllowReactApp");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using AutoEntity.EntityModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container
builder.Services.AddControllers();

builder.Services.AddDbContext<MasterPorterContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("EC"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EC"))
    ));

// Updated CORS: Allow localhost for dev and your deployed domain for production
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin() // Or specify your frontend production URL here
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Build the app instance
var app = builder.Build();

// 3. Configure the HTTP request pipeline
// ENABLE SWAGGER FOR ALL ENVIRONMENTS (Production & Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = "swagger"; // Serves UI at /swagger/index.html
});

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();