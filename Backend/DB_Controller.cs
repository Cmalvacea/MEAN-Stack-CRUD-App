using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.OpenApi.Exceptions;
using Microsoft.OpenApi.Validations;


namespace DBControl {
    class CRUD_Control {
    public void CreateTask(string Name, string Details) {
        var Success = (string Mg) => Mg;
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Task = new BsonDocument{
            {"Task_Name", Name},
            {"Task_Details", Details}
        };
        var filter = Builders<BsonDocument>.Filter.Eq("Task_Name", Name);
        var Check = Collection.Find(filter);
        var Check_Count = Check.CountDocuments();
        int CorrectCount = (int) Check_Count;
        if(CorrectCount >= 1) {
            throw new InvalidOperationException("There is already a task with this name");
        } else {
            Collection.InsertOne(Task);
        }
    }

 

    public void UpdateTaskForName(string OldName, string NewName) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        var N_Update = Builders<BsonDocument>.Update.Set("Task_Name", NewName);
        var Query = Collection.Find(Filter).CountDocuments();
        if(Query == 0) {
            throw new InvalidOperationException("This task does not exist");
        } else {
            Collection.UpdateOne(Filter, N_Update);
        }
    }

    public void UpdateTaskForDetails(string OldName, string NewDetails) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        var D_Update = Builders<BsonDocument>.Update.Set("Task_Details", NewDetails);
        var Query = Collection.Find(Filter).CountDocuments();
        if(Query == 0) {
            throw new InvalidOperationException("This task does not exist");
        } else {
            Collection.UpdateOne(Filter, D_Update);
        }
    }

    public void DeleteTask(string OldName) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        var Query = Collection.Find(Filter).CountDocuments();
        if(Query == 0) {
            throw new InvalidOperationException("This task does not exist");
        } else {
            Collection.DeleteOne(Filter);
        }
    }
};
}
