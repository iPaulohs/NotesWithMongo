using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;

namespace Notes.Repository.Notes;

public interface INotesRepository
{
    public Task CreateNote(NoteInputInclude _noteInput);
    public Task<List<Note>> GetAllNotesAsync(string userId);
    public Task DeleteNote(string noteId);
    public Task<List<Note>> GetNoteByTitle(string searchTerm);
    public void EditNote(string noteId, NoteInputUpdate updatedNote);
}
