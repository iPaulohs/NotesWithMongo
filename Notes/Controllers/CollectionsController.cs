using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Repository.Collections;

namespace Notes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionsRepository _collectionsRepository;

        public CollectionsController(ICollectionsRepository collectionsRepository) => _collectionsRepository = collectionsRepository;

        [HttpGet]
        public Task<List<Collection>> Get(string authorId)
        {
            return _collectionsRepository.GetAllCollectionsAsync(authorId);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCollection(CollectionInput _collectionInput, string authorId)
        {
            var result = _collectionsRepository.CreateCollection(_collectionInput, authorId);
            return CreatedAtAction(nameof(Get), new { _collectionInput.Title }, _collectionInput);
        }

    }
}
