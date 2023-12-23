using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;
using Notes.Repository.Collections;

namespace Notes.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionsRepository _collectionsRepository;

    public CollectionsController(ICollectionsRepository collectionsRepository) => _collectionsRepository = collectionsRepository;

    [HttpPost("addcollection")]
    public async Task<IActionResult> AddCollection([FromBody] CollectionInputInclude _collectionInput)
    {
        await _collectionsRepository.CreateCollection(_collectionInput);
        return Ok(new { Message = "Collection criada com sucesso." });
    }

    [HttpDelete("delete/{collectionId}")]
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

    [HttpGet("{authorId}")]
    public async Task<List<Collection>> Get(string authorId)
    {
        return await _collectionsRepository.GetAllCollectionsAsync(authorId);
    }

    [HttpPut("edit/{collectionId}")]
    public async Task<IActionResult> EditCollection(string collectionId, [FromBody] CollectionInputUpdate updatedCollection)
    {
         _collectionsRepository.EditCollection(collectionId, updatedCollection);
        return Ok("Collection editada com sucesso.");
    }
}
