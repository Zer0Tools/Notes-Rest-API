using Zer0Tools.NotesWebAPI.Models;
using Zer0Tools.NotesWebAPI.Repositories.DTO;
namespace Zer0Tools.NotesWebAPI.Repositories
{
    public interface INotesRepository : IRepository
    {  
        
        Task<List<NoteModel>> GetNotesAsync();
        //Task<List<NoteModel>> GetNotesAsync(DateTime dateA, DateTime dateB);
        Task<NoteModel> GetNoteAsync(Guid noteId);

        Task<bool> HasNoteAsync(Guid noteId);
        Task UpdateNoteAsync(NoteDTO noteDTO, Guid noteId);
        Task<Guid> AddNoteAsync(NoteDTO noteDTO);
        Task DeleteNoteAsync(Guid noteId);
    }
}