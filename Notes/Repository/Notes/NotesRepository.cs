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

    public async Task CreateNote(NoteInput _noteInput, string authorId, string collectionId)
    {
        var user = _context.Users.FirstOrDefault(author => author.Id == authorId);

        Note note = new()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = _noteInput.Title,
            Description = _noteInput.Description,
            AuthorId = authorId,
            CollectionId = _noteInput.CollectionId,
        };

        await _notes.InsertOneAsync(note);
        //TODO: Adicionar o ID da nota criada ao respectivo array BSON da coleção
    }

    public async Task<List<Note>> GetAllNotesAsync(string authorId)
    {
        return await _notes.Find(note => note.AuthorId == authorId).ToListAsync();    
    }
}
