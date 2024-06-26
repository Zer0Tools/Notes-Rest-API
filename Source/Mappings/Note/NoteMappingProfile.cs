

using System.Globalization;

namespace Zer0Tools.NotesWebAPI.Mappings
{
    public class NoteMappingProfile : Profile, IMappingProfile
    {
        public NoteMappingProfile()
        {
            CreateMap<API.DTO.NoteDTO, Repositories.DTO.NoteDTO>()
            .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Repositories.DTO.NoteDTO, Models.NoteModel>();
        }
    }
}