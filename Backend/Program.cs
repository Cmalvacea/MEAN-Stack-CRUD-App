using Microsoft.OpenApi.Models;
using DBControl;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using Newtonsoft.Json;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var NewController = new CRUD_Control();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5500");
                      });
});


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);


var Client = new MongoClient("mongodb://localhost:27017");
var DataB = Client.GetDatabase("D-NET-DB");
var Collection = DataB.GetCollection<BsonDocument>("Tasks");


app.MapGet("/Task", () => {
    var DList = Collection.Find(new BsonDocument()).ToList();
    var SList = JsonConvert.SerializeObject(DList);
    return SList;
});

app.MapPost("/Task/{Name}/{Details}", (String Name, String Details) => {
    try
    {
        NewController.CreateTask(Name, Details);
    }
    catch (System.Exception ex)
    {
        var ThrowFail = () => $"{ex}";
    }
    finally {
        Results.Text("Task created succesfully");
    }
});
app.MapDelete("/Task/Delete/{name}", (string name) => {
    try
    {
        NewController.DeleteTask(name);
    }
    catch (System.Exception ex)
    {
        var ThrowFail = () => $"{ex}";
    }
    finally {
        Results.Text("Task deleted succesfully");
    }
});
app.MapPut("/Task/UpdateName/{name}/{newName}", (string name, string NewName) => {
    try
    {
        NewController.UpdateTaskForName(name, NewName);
    }
    catch (System.Exception ex)
    {
        var ThrowFail = () => $"{ex}";
    }
    finally {
        Results.Text("Task updated succesfully");
    }
});
app.MapPut("/Task/UpdateDetails/{name}/{newDetails}", (string name, string NewDetails) => {
    try
    {
        NewController.UpdateTaskForDetails(name, NewDetails);
    }
    catch (System.Exception ex)
    {
        var ThrowFail = () => $"{ex}";
    }
    finally {
        Results.Text("Task updated succesfully");
    }
});



app.Run();


