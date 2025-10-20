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
            // 1. IGNORER l'ID, car il est généré par la base de données lors de la création
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                // 2. CORRECTION DES CHAMPS REQUIS MANQUANTS DANS LE DTO :
                // Director est requis dans School mais absent dans SchoolDto
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => string.Empty))

                // Sections est requis dans School mais absent dans SchoolDto
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => string.Empty))

                // WebSite est absent dans SchoolDto mais pourrait être non-nullable (bien que marqué string?)
                .ForMember(dest => dest.WebSite, opt => opt.MapFrom(src => string.Empty));
               
        }
    }
}