using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MultiLanguageExamManagementSystem.Models.Dtos.Language;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizationsController : ControllerBase
    {
        private readonly ILogger<LocalizationsController> _logger;
        private readonly ICultureService _cultureService;

        public LocalizationsController(ILogger<LocalizationsController> logger, ICultureService cultureService)
        {
            _logger = logger;
            _cultureService = cultureService;
        }

        // Your code here

        [HttpGet(Name = "GetLocalizationResource")]
        public async Task<IActionResult> GetLocalizationResource()
        {
            // implement the logic that allows us to call the culture service like this, note that the string we are 
            // sending "ne.1" it is of form namespace.key, in this case "ne" is the namespace and "1" is the key
            // so you should return back the localization resource that is having this namespace and key and the
            // language code based in the request header

          //  var message = _cultureService["ne.1"];
            //var message = _cultureService.GetString("ne.1").Value; // Implement this too

          return Ok();
        }


        #region Languages


        [HttpGet("languages/{id}")]
        public async Task<IActionResult> GetLanguageById(int id)
        {
            var language = await _cultureService.GetLanguage(id);
            if (language == null)
            {
                return NotFound("Language not found.");
            }
            return Ok(language);
        }


        [HttpGet("languages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _cultureService.GetAllLanguages();
            return Ok(languages);
        }


        [HttpGet("languagesListView")]
        public async Task<IActionResult> LanguagesListView(string? searchText, int page = 1, int pageSize = 10)
        {
            var languages = await _cultureService.LanguagesListView(searchText, page, pageSize);
            return Ok(languages);
        }


        [HttpPut("language")]
        public async Task<IActionResult> UpdateLanguage(LanguageDto languageToUpdate)
        {
            await _cultureService.UpdateLanguage(languageToUpdate);
            return Ok("Language updated successfully.");
        }


        [HttpDelete("language/{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            await _cultureService.DeleteLanguage(id);
            return Ok("Language deleted successfully.");
        }

        #endregion









        // Your code here
        // Implement endpoints for crud operations (no relation to localization needed here, just normal cruds)
        // Except when adding a new language(or localization resource), you should get all the existing localization resources in english
        // prepare and translate them to the new language using translation service which should use this api for translations:
        // https://www.deepl.com/pro-api?cta=header-pro-api, and then seed the new created localizations for the new added language

        // same applies when adding a new localization resource, for example you implement the code for adding a new
        // language resource, then you call the api to prepare the translated resource to all your existing languages
        // and then you add the same resource for all the languages
    }
}
