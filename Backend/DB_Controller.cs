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
};
}
