using MongoDB.Bson;
using MongoDB.Driver;


namespace DBControl {
    class CRUD_Control {
    public void CreateTask(string Name, string Details) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Task = new BsonDocument{
            {"Task_Name", Name},
            {"Task_Details", Details}
        };
        Collection.InsertOne(Task);
    }

    public void GetTasksU() {

        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var DocumentList = Collection.Find(new BsonDocument()).ToList();
        System.Console.WriteLine(DocumentList);

    }

    public void UpdateTaskForName(string OldName, string NewName) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        var N_Update = Builders<BsonDocument>.Update.Set("Task_Name", NewName);
        Collection.UpdateOne(Filter, N_Update);
    }

    public void UpdateTaskForDetails(string OldName, string NewDetails) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        var D_Update = Builders<BsonDocument>.Update.Set("Task_Details", NewDetails);
        Collection.UpdateOne(Filter, D_Update)
    }

    public void DeleteTask(string OldName) {
        var Client = new MongoClient("mongodb://localhost:27017");
        var DataB = Client.GetDatabase("D-NET-DB");
        var Collection = DataB.GetCollection<BsonDocument>("Tasks");
        var Filter = Builders<BsonDocument>.Filter.Eq("Task_Name", OldName);
        Collection.DeleteOne(Filter);
    }
};
}
