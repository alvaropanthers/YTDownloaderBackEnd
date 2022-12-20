using Microsoft.EntityFrameworkCore;
using YTDownloaderAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var policyName = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        op =>
        {
            op.WithOrigins(
                "http://localhost:3000",
                "http://localhost:3001",
                "http://palace-dev:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<PlayListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();

app.Run();