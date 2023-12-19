using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;

namespace Notes.Repository.Notes;

public interface INotesRepository
{
    public Task CreateNote(NoteInput _noteInput, string userId, string collectionId);
    public Task<List<Note>> GetAllNotesAsync(string userId);
}
