using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Notes.Database;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Identity;

namespace Notes.Repository.Collections;

public class CollectionsRepository : ICollectionsRepository
{
    private readonly IMongoCollection<Collection> _collections;
    private readonly IMongoCollection<Note> _notes;
    private readonly AuthDbContext _context;

    public CollectionsRepository(IOptions<MongoContext> options, AuthDbContext context)
    {
        MongoClient _mongoClient = new(options.Value.MongoConnectionString);
        var _database = _mongoClient.GetDatabase(options.Value.DatabaseName);
        _collections = _database.GetCollection<Collection>(options.Value.CollectionCollections);
        _notes = _database.GetCollection<Note>(options.Value.CollectionNotes);
        _context = context;
    }

    public async Task CreateCollection(CollectionInput _collectionInput)
    {
        var user = _context.Users.FirstOrDefault(user => user.Id == _collectionInput.AuthorId);

        if (user != null)
        {
            Collection collection = new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Title = _collectionInput.Title,
                Description = _collectionInput.Description,
                NotesId = new List<string>(),
                AuthorId = user.Id
            };

            await _collections.InsertOneAsync(collection);
        }

    }

    public async Task<List<Collection>> GetAllCollectionsAsync(string authorId)
    {
        var filter = Builders<Collection>.Filter.Eq(x => x.AuthorId, authorId);
        return await _collections.Find(filter).ToListAsync();
    }

    public void DeleteCollection(string collectionId)
    {
        var filter = Builders<Collection>.Filter.Eq(x => x.Id, collectionId);
        var filterNotes = Builders<Note>.Filter.Eq(x => x.CollectionId, collectionId);

        _collections.DeleteOneAsync(filter);
        _notes.DeleteManyAsync(filterNotes);
    }

    public async Task<List<Collection>> GetCollectionByTitle(string searchTerm)
    {
        var regexFilter = Builders<Collection>.Filter.Regex("Title", new BsonRegularExpression($".*{searchTerm}.*", "i"));
        return await _collections.Find(regexFilter).ToListAsync();
    }
}
