namespace Zer0Tools.NotesWebAPI.Models
{
    public class NoteModel : Model
    {
        public string Title {get; set;} = string.Empty;

        public string Details {get; set;} = string.Empty;

        public DateTime CreateDate {get; set;}

        public DateTime? UpdateDate {get; set;} = null;
    }
}


