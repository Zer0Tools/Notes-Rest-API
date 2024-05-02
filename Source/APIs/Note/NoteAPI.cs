using Zer0Tools.NotesWebAPI.Repositories;
using Zer0Tools.NotesWebAPI.API.DTO;
using Zer0Tools.NotesWebAPI.Models;
namespace Zer0Tools.NotesWebAPI.API
{
    public class NoteAPI : API
    {
        public override void Register(WebApplication app)
        {
            app.MapGet("/notes/get/all", GetAllNotes)
                .Produces<List<NoteModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Get all notes")
                .WithTags("Get");
                
            app.MapGet("/notes/get/{id}", GetById)
                .Produces<NoteModel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("Get note")
                .WithTags("Get");
       
            app.MapPost("/notes/create", CreateNewNote)
                .Accepts<NoteDTO>("application/json")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Create note post")
                .WithTags("Create");

            app.MapPut("/notes/create", CreateNewNote)
                .Accepts<NoteDTO>("application/json")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Create note put")
                .WithTags("Create");

            app.MapPut("/notes/update/{id}", UpdateNote)
                .Accepts<NoteDTO>("application/json")
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status200OK)
                .WithName("Update note")
                .WithTags("Update");                
                
            app.MapDelete("/notes/delete/{id}", DeleteNote)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Delete note")
                .WithTags("Delete");        
        }

        private async Task<IResult> GetAllNotes(HttpContext context, [FromServices] NotesRepository repository)
        {
            return Results.Ok(await repository.GetNotesAsync());
        }
        private async Task<IResult> GetById(HttpContext context, [FromServices] NotesRepository repository, Guid id)
        {
            if(!(await repository.HasNoteAsync(id))) 
                return Results.NotFound();
            var note = await repository.GetNoteAsync(id);
            return Results.Ok(note);
        }
        private async Task<IResult> CreateNewNote(HttpContext context, [FromServices] NotesRepository repository, IMapper mapper, [FromBody] NoteDTO noteDTO)
        {
            var noteId = await repository.AddNoteAsync(mapper.Map<NoteDTO, Repositories.DTO.NoteDTO>(noteDTO));
            return Results.Ok(noteId);  
        }
        private async Task<IResult> UpdateNote(HttpContext context, [FromServices] NotesRepository repository, IMapper mapper, [FromBody] NoteDTO noteDTO, Guid id)
        {
            if(!(await repository.HasNoteAsync(id))) return Results.NotFound();
            await repository.UpdateNoteAsync(mapper.Map<NoteDTO, Repositories.DTO.NoteDTO>(noteDTO), id);
            return Results.Ok();
        }
        private async Task<IResult> DeleteNote(HttpContext context, [FromServices] NotesRepository repository, Guid id)
        {
            if(!(await repository.HasNoteAsync(id))) return Results.BadRequest("Note not found");
            await repository.DeleteNoteAsync(id);
            return Results.NoContent();
        }

    }
}