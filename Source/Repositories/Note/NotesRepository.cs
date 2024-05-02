using Zer0Tools.NotesWebAPI.Models;
using Zer0Tools.NotesWebAPI.Repositories.DTO;
namespace Zer0Tools.NotesWebAPI.Repositories
{
    public class NotesRepository : Repository<NoteModel>
    {
        private readonly IMapper _mapper;
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
            var model = await GetNoteAsync(noteId);
            model.UpdateDate = noteDTO.RequestDate;
            model.Title = noteDTO.Title;
            model.Details = noteDTO.Details;
            await base.UpdateItemAsync(model);
        }

        public async Task DeleteNoteAsync(Guid noteId) => await base.DeleteItemAsync(noteId);
    }
}