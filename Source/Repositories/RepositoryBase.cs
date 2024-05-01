
using Zer0Tools.NotesWebAPI.Models;

namespace Zer0Tools.NotesWebAPI.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : ModelBase
    {
        protected readonly ApplicationContext _context;
        
        public RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }
        public virtual async Task<List<T>> GetAllItems() => await _context.Set<T>().ToListAsync<T>();
        public virtual async Task<T> GetItemAsync(Guid id)
        {
            T? Tmodel = await _context.FindAsync<T>(new object[] {id});
            return Tmodel is null
                ? throw new KeyNotFoundException("model not found")
                : Tmodel; 
        }
        public virtual async Task AddItemAsync(T model)
        {
            T? Tmodel = await _context.FindAsync<T>(new object[] {model.Id});    
            if(Tmodel is null) await _context.AddAsync<T>(model);  
            else throw new ArgumentException("same model"); 
            await SaveAsync();
        }
        public virtual async Task AddItemsAsync(params T[] list)
        {
            foreach(var model in list)
            {
                T? Tmodel = await _context.FindAsync<T>(new object[] {model.Id});    
                if(Tmodel is null) await _context.AddAsync<T>(model);  
                else throw new ArgumentException("same model"); 
            }
            await SaveAsync();
        }

        public virtual async Task<bool> HasItemAsync(Guid id)
        {
            T? Tmodel = await _context.FindAsync<T>(new object[] {id});    
            return Tmodel is not null;
        }

        public virtual async Task DeleteItemAsync(Guid id)
        {
            T? Tmodel = await _context.FindAsync<T>(new object[] {id});
            if(Tmodel is null) throw new KeyNotFoundException("model not found");
            _context.Remove<T>(Tmodel);
            await SaveAsync();
        }
        public virtual async Task UpdateItemAsync(T newModel)
        {
            T? Tmodel = await _context.FindAsync<T>(new object[] {newModel.Id});
            if(Tmodel is null) throw new KeyNotFoundException("model not found");
            _context.Remove<T>(Tmodel);
            _context.Add(newModel);
            await SaveAsync();      
        }

    public virtual async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}