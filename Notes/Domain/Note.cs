using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Notes.Domain;

public class Note
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? CollectionId { get; set; }

    public string? AuthorId { get; set; }
}
