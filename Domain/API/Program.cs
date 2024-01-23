using API.Common.Error;
using API.Filter;
using API.Middleware;
using Application;
using Application.Authentication;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
}

// Add services to the container.

//cach 2
builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();


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

//cach 1
//app.UseMiddleware<ErrorHandlingMiddleware>();

//cach 3
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
