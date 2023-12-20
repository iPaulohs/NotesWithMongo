using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;
using Notes.Repository.Notes;

namespace Notes.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository) => _notesRepository = notesRepository;


        [HttpGet]
        public Task<List<Note>> Get(string authorId)
        {
            return _notesRepository.GetAllNotesAsync(authorId);
        } 

        [HttpPost("add")]
        public async Task<IActionResult> AddNote(NoteInput _noteInput)
        {
            var result = _notesRepository.CreateNote(_noteInput);
            return CreatedAtAction(nameof(Get), new { _noteInput.Title }, _noteInput);
        }

        [HttpDelete]
        public void DeleteNote(string noteId)
        {
            _notesRepository.DeleteNote(noteId);
        }
    }
}
