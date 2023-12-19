using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Notes.DataTransfer.Input.CollectionDataTransfer
{
    public class CollectionInput
    {
        [BsonRepresentation(BsonType.String)]
        public string? Title { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string? AuthorId { get; set; }
    }
}
