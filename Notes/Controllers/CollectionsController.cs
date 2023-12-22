using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Repository.Collections;

namespace Notes.Controllers;

[Authorize]
[Route("[controller]")]
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
    public async Task<IActionResult> AddCollection(CollectionInput _collectionInput)
    {
        await _collectionsRepository.CreateCollection(_collectionInput);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCollection(string collectionId)
    {
        _collectionsRepository.DeleteCollection(collectionId);
        return Ok("Collection apagada.");
    }

    [HttpGet("search/{searchTerm}")]
    public async Task<List<Collection>> GetCollectionByTitle(string searchTerm)
    {
        return await _collectionsRepository.GetCollectionByTitle(searchTerm);
    }


}
