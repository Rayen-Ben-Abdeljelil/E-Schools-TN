using AutoMapper;
using SchoolAPI.Models;

namespace SchoolAPI.DTOs
{
    public class SchoolsAutoMapperProfile : Profile 
    {
        public SchoolsAutoMapperProfile()
        {
            // Mappage pour la lecture (Mapping Classe du domaine → DTO)
            CreateMap<School, SchoolDto>();

            // Mappage pour l'écriture (Mapping DTO → Classe du domaine)
            CreateMap<SchoolDto, School>()
            //SchoolDto n'a pas les champs (Director, Website, etc.) et ils ne peuvent pas être nuls.
            
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => string.Empty))
            
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => string.Empty))

            // Correction pour 'WebSite' (si elle est aussi non-nullable dans la BDD)
            .ForMember(dest => dest.WebSite, opt => opt.MapFrom(src => string.Empty))

            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => 0));
        }
    }
}