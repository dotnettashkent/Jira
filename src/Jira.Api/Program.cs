using Jira.Api.Extensions;
using Jira.Api.Middlewares;
using Jira.DAL.Contexts;
using Jira.Service.Helpers;
using Jira.Service.Mappers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<JiraDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<EmailVerification>();
builder.Services.AddCustomServices();

// Swagger setup
builder.Services.AddSwaggerService();

// Jwt services
builder.Services.AddJwtService(builder.Configuration);

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

EnvironmentHelper.WebHostPath =
    app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  middleware 
app.UseMiddleware<ExeptionHandlerMiddleWare>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();