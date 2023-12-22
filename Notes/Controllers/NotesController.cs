using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;
using Notes.Repository.Notes;

namespace Notes.Controllers;

[ApiController]
[Authorize]
[Route("/Notes")]
public class NotesController : ControllerBase
{
    private readonly INotesRepository _notesRepository;

    public NotesController(INotesRepository notesRepository) => _notesRepository = notesRepository;

    [HttpPost("addnote")]
    public async Task<IActionResult> AddNote([FromBody] NoteInput _noteInput)
    {
        await _notesRepository.CreateNote(_noteInput);
        return Ok(new { Message = "Nota criada com sucesso." });
    }

    [HttpDelete("delete/{noteId}")]
    public void DeleteNote(string noteId)
    {
        _notesRepository.DeleteNote(noteId);
    }

    [HttpGet("search/{searchTerm}")]
    public async Task<List<Note>> GetNoteByTitle(string searchTerm)
    {
        return await _notesRepository.GetNoteByTitle(searchTerm);
    }

    [HttpGet("{authorId}")]
    public Task<List<Note>> Get(string authorId)
    {
        return _notesRepository.GetAllNotesAsync(authorId);
    }
}
