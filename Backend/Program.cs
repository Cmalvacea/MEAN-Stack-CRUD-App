using Microsoft.OpenApi.Models;
using DBControl;

var builder = WebApplication.CreateBuilder(args);
var NewController = new CRUD_Control();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD Angular/.NET", Description = "An odd CRUD for showcase", Version = "v1" });
});



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Minimal API v1");
});

NewController.CreateTask("Task 1", "Task 1 details");


app.MapGet("/", () => "Hello World!");

app.MapGet("/ArgTest/{id}", (String id) => "Hello " + id);

app.Run();


