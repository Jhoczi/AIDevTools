using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using PB.MeetUp.AITools.Mongo;

namespace PB.MeetUp.AITools.Models;

public class Book : IEntity<string>
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}