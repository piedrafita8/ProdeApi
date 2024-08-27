using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProdeApi.Models;

public class Match
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    
    [BsonElement("homeTeamGoals")]
    public string homeTeamGoals { get; set; }
    
    [BsonElement("awayTeamGoals")]
    public string awayTeamGoals { get; set; }
    
    [BsonElement("userId")]
    public string userId { get; set; }
}