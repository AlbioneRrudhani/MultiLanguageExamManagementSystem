using LifeEcommerce.Helpers;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ICultureService
    {


        #region String Localization
        LocalizationResourceDto this[string namespaceKey] { get; }
        LocalizationResourceDto GetString(string namespaceKey);
        #endregion


        #region Languages
        Task CreateLanguage(CreateLanguageDto languageToCreate);
        Task<List<LanguageDto>> GetAllLanguages();
        Task<LanguageDto> GetLanguage(int id);
        Task<PagedInfo<LanguageDto>> LanguagesListView(string search, int page = 1, int pageSize = 10);
        Task UpdateLanguage(LanguageDto languageToUpdate);
        Task DeleteLanguage(int id);
        #endregion


        #region Localization Resources

        Task CreateLocalizationResource(CreateLocalizationResourceDto resourceToCreate);
        Task<LocalizationResourceDto> GetLocalizationResource(string ns, string key, string languageCode);
        Task<List<LocalizationResourceDto>> GetAllLocalizationResource();
        Task<PagedInfo<LocalizationResourceDto>> LocalizationResourcesListView(string search, int page = 1, int pageSize = 10);
        Task UpdateLocalizationResource(LocalizationResourceDto resourceToUpdate);
        Task DeleteLocalizationResource(int id);
        #endregion
    }
}
