using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;

namespace Notes.Repository.Collections
{
    public interface ICollectionsRepository
    {
        public Task CreateCollection(CollectionInput _collectionInput);
        public Task<List<Collection>> GetAllCollectionsAsync(string userId);
        public void DeleteCollection(string collectionId);
    }
}
