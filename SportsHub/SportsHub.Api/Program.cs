using Microsoft.AspNetCore.Diagnostics;
using SportsHub.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "/error";
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        if (feature is not null)
        {
           
            await context.Response.WriteAsync(new ErrorHandler
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please Try Again Later"

            }.ToString());
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
