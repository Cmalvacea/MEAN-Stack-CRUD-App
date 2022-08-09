using Microsoft.OpenApi.Models;
using DBControl;

var builder = WebApplication.CreateBuilder(args);
var NewController = new CRUD_Control();


var app = builder.Build();







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


