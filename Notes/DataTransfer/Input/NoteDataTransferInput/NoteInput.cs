using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Notes.DataTransfer.Input.NoteDataTransferInput
{
    public class NoteInput
    {
        [BsonRepresentation(BsonType.String)]
        public string? Title { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string? CollectionId { get; set; }
    }
}
