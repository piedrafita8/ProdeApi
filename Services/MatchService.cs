using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProdeApi.Models;

namespace ProdeApi.Services;

public class MatchService
{
    private readonly IMongoCollection<Match> _matchsCollection;

    public MatchService(IOptions<MongoDBSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DbName);
        _matchsCollection = database.GetCollection<Match>("Matchs");
    }
    
    public async Task CreateAsync(Match match)
    {
        await _matchsCollection.InsertOneAsync(match);
        return;
    }

    public async Task<List<Match>> GetAsync()
    {
        return await _matchsCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task<Match> GetOneAsync(string id)
    {
        var match = _matchsCollection.Find(x => x.id == id);
        
        if (match !=  null)
        {
            return await _matchsCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        }
        else
        {
            throw new Exception("Match not found");
        }
        
        
    }
    
    public async Task UpdateAsync(string id, Match updatedMatch)
    {
        var filter = Builders<Match>.Filter.Eq(x => x.id, id);
        var matchToUpdate = _matchsCollection.Find(x => x.id == id);
        if (matchToUpdate !=  null)
        {
            await _matchsCollection.ReplaceOneAsync(filter, updatedMatch);
        }
        else
        {
            throw new Exception("Match not found");
        }
    }

    
    public async Task DeleteAsync(string id)
    {
        var matchToDelete = _matchsCollection.Find(x => x.id == id);
        
        if (matchToDelete !=  null)
        {
            await _matchsCollection.DeleteOneAsync(x => x.id == id);
        }
        else
        {
            throw new Exception("Match not found");
        }
        
      
        return;
    }
}