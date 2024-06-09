using AutoMapper;
using LifeEcommerce.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource;


//using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Services.IServices;
using System.Globalization;

namespace MultiLanguageExamManagementSystem.Services
{
    public class CultureService : ICultureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CultureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Your code here

        #region String Localization

        // String localization methods implementation here

        #endregion


        #region Languages

        public async Task<LanguageDto> GetLanguage(int id)
        {
            var language = await _unitOfWork.Repository<Language>().GetById(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<LanguageDto>(language);
        }


        public async Task<List<LanguageDto>> GetAllLanguages()
        {
            var languages = await _unitOfWork.Repository<Language>().GetAll().ToListAsync();
            return _mapper.Map<List<LanguageDto>>(languages);
        }


        public async Task<PagedInfo<LanguageDto>> LanguagesListView(string search, int page = 1, int pageSize = 10)
        {
            var languages = _unitOfWork.Repository<Language>()
                                        .GetAll()
                                        .WhereIf(!string.IsNullOrEmpty(search), x => x.Name.Contains(search));

            var count = await languages.CountAsync();
            var items = _mapper.Map<List<LanguageDto>>(await languages.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());

            var languagesPaged = new PagedInfo<LanguageDto>()
            {
                TotalCount = count,
                Page = page,
                PageSize = pageSize,
                Items = items
            };
            return languagesPaged;
        }


        public async Task UpdateLanguage(LanguageDto languageToUpdate)
        {
            var language = await _unitOfWork.Repository<Language>().GetById(x => x.Id == languageToUpdate.Id).FirstOrDefaultAsync();

            _mapper.Map(languageToUpdate, language);
            _unitOfWork.Repository<Language>().Update(language);
            _unitOfWork.Complete();
        }

        public async Task DeleteLanguage(int id)
        {
            var languageToDelete = await _unitOfWork.Repository<Language>().GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
            _unitOfWork.Repository<Language>().Delete(languageToDelete);
            _unitOfWork.Complete();
        }

        #endregion


        #region Localization Resources

        public async Task<List<LocalizationResourceDto>> GetAllLocalizationResource()
        {
            var localizationResources = await _unitOfWork.Repository<LocalizationResource>().GetAll().ToListAsync();
            return _mapper.Map<List<LocalizationResourceDto>>(localizationResources);
        }


        public async Task<PagedInfo<LocalizationResourceDto>> LocalizationResourcesListView(string search, int page = 1, int pageSize = 10)
        {
            var localizationResources = _unitOfWork.Repository<LocalizationResource>()
                                        .GetAll()
                                        .WhereIf(!string.IsNullOrEmpty(search), x => x.Key.Contains(search));

            var count = await localizationResources.CountAsync();
            var items = _mapper.Map<List<LocalizationResourceDto>>(await localizationResources.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());

            var resourcesPaged = new PagedInfo<LocalizationResourceDto>()
            {
                TotalCount = count,
                Page = page,
                PageSize = pageSize,
                Items = items
            };
            return resourcesPaged;
        }


        public async Task UpdateLocalizationResource(LocalizationResourceDto resourceToUpdate)
        {
            var existingResource = await _unitOfWork.Repository<LocalizationResource>().GetById(x => x.Id == resourceToUpdate.Id).FirstOrDefaultAsync();
            _mapper.Map(resourceToUpdate, existingResource);
            _unitOfWork.Repository<LocalizationResource>().Update(existingResource);
            _unitOfWork.Complete();
        }

        public async Task DeleteLocalizationResource(int id)
        {
            var localizationResourceToDelete = await _unitOfWork.Repository<Language>().GetById(x => x.Id == id).FirstOrDefaultAsync();
            _unitOfWork.Repository<Language>().Delete(localizationResourceToDelete);
            _unitOfWork.Complete();
        }

        #endregion
    }
}
