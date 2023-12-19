using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Notes.Database;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Identity;

namespace Notes.Repository.Collections
{
    public class CollectionsRepository : ICollectionsRepository
    {
        private readonly IMongoCollection<Collection> _collections;
        private readonly AuthDbContext _context;

        public CollectionsRepository(IOptions<MongoContext> options, AuthDbContext context)
        {
            MongoClient _mongoClient = new(options.Value.MongoConnectionString);
            var _database = _mongoClient.GetDatabase(options.Value.DatabaseName);
            _collections = _database.GetCollection<Collection>(options.Value.CollectionCollections);
            _context = context;
        }

        public async Task CreateCollection(CollectionInput _collectionInput, string userId)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);

            Collection collection = new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Title = _collectionInput.Title,
                Description = _collectionInput.Description,
                NotesId = new BsonArray(),
                AuthorId = _collectionInput.AuthorId
            };

            await _collections.InsertOneAsync(collection);
        }

        public async Task<List<Collection>> GetAllCollectionsAsync(string userId)
        {
            return await _collections.Find(collection => collection.AuthorId == userId).ToListAsync();
        }
    }
}
