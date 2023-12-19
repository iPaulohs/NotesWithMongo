using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Notes.Domain;

public class Collection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public object? NotesId { get; internal set; }

    public string? AuthorId { get; set; }
}
