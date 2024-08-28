using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProdeApi.Models;

namespace ProdeApi.Services;

public class PredictionService
{
    private readonly IMongoCollection<UserPrediction> _predictionsCollection;

    public PredictionService(IOptions<MongoDBSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DbName);
        _predictionsCollection = database.GetCollection<UserPrediction>("Predictions");
    }
    
    public async Task CreateAsync(UserPrediction userPrediction)
    {
        await _predictionsCollection.InsertOneAsync(userPrediction);
        return;
    }

    public async Task<List<UserPrediction>> GetAsync()
    {
        return await _predictionsCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task<UserPrediction> GetOneAsync(string id)
    {
        var matchPrediction = _predictionsCollection.Find(x => x.id == id);
        
        if (matchPrediction !=  null)
        {
            return await _predictionsCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        }
        else
        {
            throw new Exception("Prediction not found");
        }
        
        
    }
    
    public async Task UpdateAsync(string id, UserPrediction updatedUserPrediction)
    {
        var filter = Builders<UserPrediction>.Filter.Eq(x => x.id, id);
        var predictionToUpdate = _predictionsCollection.Find(x => x.id == id);
        if (predictionToUpdate !=  null)
        {
            await _predictionsCollection.ReplaceOneAsync(filter, updatedUserPrediction);
        }
        else
        {
            throw new Exception("Prediction not found");
        }
    }

    
    public async Task DeleteAsync(string id)
    {
        var predictionToDelete = _predictionsCollection.Find(x => x.id == id);
        
        if (predictionToDelete !=  null)
        {
            await _predictionsCollection.DeleteOneAsync(x => x.id == id);
        }
        else
        {
            throw new Exception("Prediction not found");
        }
        
      
        return;
    }
}