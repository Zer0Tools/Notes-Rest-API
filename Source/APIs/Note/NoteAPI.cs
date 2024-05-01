using Microsoft.AspNetCore.Authorization;
using Zer0Tools.NotesWebAPI.Repositories;
using Zer0Tools.NotesWebAPI.API.DTO;
using Zer0Tools.NotesWebAPI.Models;
namespace Zer0Tools.NotesWebAPI.API
{
    public class NoteAPI : IAPI
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/notes/get/all", GetAllNotes)
                .Produces<List<NoteModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Get all notes")
                .WithTags("Getters");
                
            app.MapGet("/notes/get/{id}", GetById)
                .Produces<NoteModel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("Get note")
                .WithTags("Getters");
       

            app.MapPost("/notes/create", CreateNewNote)
                .Accepts<NoteDTO>("application/json")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Create note post")
                .WithTags("Creators");

            app.MapPut("/notes/create", CreateNewNote)
                .Accepts<NoteDTO>("application/json")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("Create note put")
                .WithTags("Creators");

            app.MapPut("/notes/update/{id}", UpdateNote)
                .Accepts<NoteDTO>("application/json")
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("Update note")
                .WithTags("Creators");                
                
            app.MapDelete("/notes/delete/{id}", DeleteNote)
                .WithName("Delete note")
                .WithTags("Deleters");        
        }

        private async Task<IResult> GetAllNotes(HttpContext context, INotesRepository repository)
        {
            return Results.Ok(await repository.GetNotesAsync());
        }
        private async Task<IResult> GetById(HttpContext context, INotesRepository repository, Guid id)
        {
            if(!(await repository.HasNoteAsync(id))) return Results.NotFound();
            var note = await repository.GetNoteAsync(id);
            return Results.Ok(note);
        }
        private async Task<IResult> CreateNewNote(HttpContext context, INotesRepository repository, IMapper mapper, [FromBody] NoteDTO noteDTO)
        {
            if(!DateTime.TryParse(noteDTO.RequestTime, out var dt)) return Results.BadRequest("Bad datetime");
            var noteId = await repository.AddNoteAsync(mapper.Map<NoteDTO, Repositories.DTO.NoteDTO>(noteDTO));
            return Results.Ok(noteId);       
        }
        private async Task<IResult> UpdateNote(HttpContext context, INotesRepository repository, IMapper mapper, [FromBody] NoteDTO noteDTO, Guid id)
        {
            if(!DateTime.TryParse(noteDTO.RequestTime, out var dt)) return Results.BadRequest("Bad datetime");
            if(!(await repository.HasNoteAsync(id))) return Results.NotFound();
            await repository.UpdateNoteAsync(mapper.Map<NoteDTO, Repositories.DTO.NoteDTO>(noteDTO), id);
            return Results.Ok();
        }
        private async Task<IResult> DeleteNote(HttpContext context, INotesRepository repository, Guid id)
        {
            if(!(await repository.HasNoteAsync(id))) return Results.BadRequest("Note not found");
            await repository.DeleteNoteAsync(id);
            return Results.NoContent();
        }

    }
}