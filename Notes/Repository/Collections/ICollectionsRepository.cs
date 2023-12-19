using Notes.DataTransfer.Input.CollectionDataTransfer;
using Notes.Domain;

namespace Notes.Repository.Collections
{
    public interface ICollectionsRepository
    {
        public Task CreateCollection(CollectionInput _collectionInput, string userId);
        public Task<List<Collection>> GetAllCollectionsAsync(string userId);
    }
}
