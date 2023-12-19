using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Repository.Collections;

namespace Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionsRepository _collectionsRepository;

        public CollectionsController(ICollectionsRepository collectionsRepository) => _collectionsRepository = collectionsRepository;

        [HttpGet]
        public Task<List<Collection>> Get(string userId)
        {
            return _collectionsRepository.GetAllCollectionsAsync(userId);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCollection(CollectionInput _collectionInput, string userId)
        {
            var result = _collectionsRepository.CreateCollection(_collectionInput, userId);
            return CreatedAtAction(nameof(Get), new { _collectionInput.Title }, _collectionInput);
        }

    }
}
