using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Notes.Database;
using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;
using Notes.Identity;

namespace Notes.Repository.Notes;

public class NotesRepository : INotesRepository
{
    private readonly IMongoCollection<Note> _notes;
    private readonly IMongoCollection<Collection> _collections;
    private readonly AuthDbContext _context;

    public NotesRepository(IOptions<MongoContext> options, AuthDbContext context)
    {
        MongoClient _mongoClient = new(options.Value.MongoConnectionString);
        var _database = _mongoClient.GetDatabase(options.Value.DatabaseName);
        _notes = _database.GetCollection<Note>(options.Value.CollectionNotes);
        _collections = _database.GetCollection<Collection>(options.Value.CollectionCollections);
        _context = context;
    }

    public async Task CreateNote(NoteInput _noteInput)
    {
        var user = _context.Users.FirstOrDefault(author => author.Id == _noteInput.AuthorId);

        Note note = new()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = _noteInput.Title,
            Description = _noteInput.Description,
            AuthorId = _noteInput.AuthorId,
            CollectionId = _noteInput.CollectionId
        };

        await _notes.InsertOneAsync(note);
        var builder = Builders<Collection>.Filter;
        var update = Builders<Collection>.Update.AddToSet(x => x.NotesId, note.Id);

        var collection = builder.Eq(x => x.AuthorId, _noteInput.AuthorId) & builder.Eq(x => x.Id, _noteInput.CollectionId);
        var result = await _collections.UpdateOneAsync(collection, update);
    }

    public async Task<List<Note>> GetAllNotesAsync(string authorId)
    {
        return await _notes.Find(note => note.AuthorId == authorId).ToListAsync();    
    }

    public void DeleteNote(string noteId)
    {
        var filter = Builders<Note>.Filter.Eq(x => x.Id, noteId);
        _notes.DeleteOneAsync(filter);
    }

    public async Task<List<Note>> GetNoteByTitle(string searchTerm)
    {
        var regexFilter = Builders<Note>.Filter.Regex("Title", new BsonRegularExpression($".*{searchTerm}.*", "i"));
        return await _notes.Find(regexFilter).ToListAsync();
    }
}
