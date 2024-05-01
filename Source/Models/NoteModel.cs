

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zer0Tools.NotesWebAPI.Models
{
    public class NoteModel : ModelBase
    {
        public string Title {get; set;} = string.Empty;

        public string Details {get; set;} = string.Empty;

        public DateTime CreateDate {get; set;}

        public DateTime? UpdateDate {get; set;} = null;
    }
}


