using AutoMapper;
using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;
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


            #region Exam
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamDto, Exam>().ReverseMap();

            CreateMap<Exam, ExamCreateDto>().ReverseMap();
            CreateMap<ExamCreateDto, Exam>().ReverseMap();

            #endregion



            #region Question
            CreateMap<Question, QuestionCreateDto>().ReverseMap();
            CreateMap<QuestionCreateDto, Question>().ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<QuestionDto, Question>().ReverseMap();
            #endregion

        }
    }
}
