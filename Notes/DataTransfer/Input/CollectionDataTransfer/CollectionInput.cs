using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notes.DataTransfer.Input.CollectionDataTransfer;

public class CollectionInput
{
    [BsonRepresentation(BsonType.String)]
    public string? Title { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string? Description { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string? AuthorId { get; set; }
}
