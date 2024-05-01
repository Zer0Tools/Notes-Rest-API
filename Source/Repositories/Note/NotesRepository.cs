using Zer0Tools.NotesWebAPI.Models;
using Zer0Tools.NotesWebAPI.Repositories.DTO;
namespace Zer0Tools.NotesWebAPI.Repositories
{
    public class NotesRepository : RepositoryBase<NoteModel>, INotesRepository
    {
        private readonly IMapper _mapper;
        private bool disposedValue;

        public NotesRepository(ApplicationContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<NoteModel>> GetNotesAsync() => await base.GetAllItems();

        public async Task<NoteModel> GetNoteAsync(Guid noteId) => await base.GetItemAsync(noteId);

        public async Task<Guid> AddNoteAsync(NoteDTO noteDTO)
        { 
            var noteModel = _mapper.Map<NoteDTO, NoteModel>(noteDTO);
            noteModel.CreateDate = noteDTO.RequestDate;
            await base.AddItemAsync(noteModel);    
            await _context.Entry<NoteModel>(noteModel).GetDatabaseValuesAsync();
            return noteModel.Id;
        }

        public async Task<bool> HasNoteAsync(Guid noteId) => await base.HasItemAsync(noteId);

        public async Task UpdateNoteAsync(NoteDTO noteDTO, Guid noteId)
        {                             
            var noteModel = _mapper.Map<NoteDTO, NoteModel>(noteDTO);
            noteModel.UpdateDate = noteDTO.RequestDate;
            noteModel.Id = noteId;
            await base.UpdateItemAsync(noteModel);
        }

        public async Task DeleteNoteAsync(Guid noteId) => await base.DeleteItemAsync(noteId);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    _context.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}