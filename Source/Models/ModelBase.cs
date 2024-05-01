using System.ComponentModel.DataAnnotations.Schema;

namespace Zer0Tools.NotesWebAPI.Models
{
    public abstract class ModelBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}
    }
}