using LifeEcommerce.Helpers;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource;
//using MultiLanguageExamManagementSystem.Models.Dtos;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ICultureService
    {
       
        #region Languages
        Task<List<LanguageDto>> GetAllLanguages();
        Task<LanguageDto> GetLanguage(int id);
        Task<PagedInfo<LanguageDto>> LanguagesListView(string search, int page = 1, int pageSize = 10);
        Task UpdateLanguage(LanguageDto languageToUpdate);
        Task DeleteLanguage(int id);
        #endregion


        #region Localization Resources
        Task<List<LocalizationResourceDto>> GetAllLocalizationResource();
        Task<PagedInfo<LocalizationResourceDto>> LocalizationResourcesListView(string search, int page = 1, int pageSize = 10);
        Task UpdateLocalizationResource(LocalizationResourceDto resourceToUpdate);
        Task DeleteLocalizationResource(int id);
        #endregion
    }
}
