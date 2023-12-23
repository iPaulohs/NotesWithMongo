using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;

namespace Notes.Repository.Collections;

public interface ICollectionsRepository
{
    public Task CreateCollection(CollectionInputInclude _collectionInput);
    public Task<List<Collection>> GetAllCollectionsAsync(string userId);
    public void DeleteCollection(string collectionId);
    public Task<List<Collection>> GetCollectionByTitle(string searchTerm);
    public void EditCollection(string collectionId, CollectionInputUpdate updatedCollection);
}
