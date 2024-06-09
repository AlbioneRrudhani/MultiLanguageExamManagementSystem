using AutoMapper;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace LifeEcommerce.Helpers
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations() 
        {
            #region Language

            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<LanguageDto, Language>().ReverseMap();

            CreateMap<Language, CreateLanguageDto>().ReverseMap();
            CreateMap<CreateLanguageDto, Language>().ReverseMap();
            
            #endregion


            #region Localization Resource

            CreateMap<LocalizationResource, LocalizationResourceDto>().ReverseMap();
            CreateMap<LocalizationResourceDto, LocalizationResource>().ReverseMap();

            CreateMap<LocalizationResource, CreateLocalizationResourceDto>().ReverseMap();
            CreateMap<CreateLocalizationResourceDto, LocalizationResource>().ReverseMap();
            
            #endregion
        }
    }
}
