using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.NoteDataTransferInput;
using Notes.Domain;
using Notes.Repository.Notes;

namespace Notes.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository) => _notesRepository = notesRepository;


        [HttpGet]
        public Task<List<Note>> Get(string userId)
        {
            return _notesRepository.GetAllNotesAsync(userId);
        } 

        [HttpPost("add")]
        public async Task<IActionResult> AddNote(NoteInput _noteInput, string userId)
        {
            var result = _notesRepository.CreateNote(_noteInput, userId);
            return CreatedAtAction(nameof(Get), new { _noteInput.Title }, _noteInput);
        }


    }
}
