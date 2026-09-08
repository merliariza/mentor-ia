using System.Reflection;
using ApiPortfolio.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddHttpClient<Application.Services.OpenRouterService>();


builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.AddAplicacionServices();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddValidationErrors();   
builder.Services.AddCustomRateLimiter();
builder.Services.AddJwt(builder.Configuration); 


builder.Services.AddDbContext<PublicDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PublicDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();  

app.Run();
