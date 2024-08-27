using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProdeApi.Models;

namespace ProdeApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IOptions<MongoDBSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DbName);
        _usersCollection = database.GetCollection<User>(mongoDbSettings.Value.CollectionName);
    }

    public async Task CreateAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> GetAsync()
    {
        return await _usersCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task DeleteAsync(string id)
    {
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
        return;
    }
}