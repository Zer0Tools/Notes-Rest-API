

namespace Zer0Tools.NotesWebAPI.Repositories
{
    public interface IRepository
    {
        public Task SaveAsync();
    }

    public interface IRepository<T> : IRepository
    {
        public Task<List<T>> GetAllItems();
        public Task<T> GetItemAsync(Guid id);
        public Task AddItemAsync(T model);
        public Task AddItemsAsync(params T[] list);

        public Task<bool> HasItemAsync(Guid id);
        public Task DeleteItemAsync(Guid id);
        public Task UpdateItemAsync(T newModel);
        
    }
}