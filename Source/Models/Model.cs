using System.ComponentModel.DataAnnotations.Schema;

namespace Zer0Tools.NotesWebAPI.Models
{
    public abstract class Model
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}
    }
}