using MultiLanguageExamManagementSystem.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MultiLanguageExamManagementSystem.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _client;
        private readonly string _authKey;
        private readonly string _apiBaseUrl = "https://api-free.deepl.com/v2/translate";

        public TranslationService(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _authKey = configuration["DeepLApiKey:ApiKey"] ?? throw new ArgumentNullException("DeepLApiKey:ApiKey");
        }

        public async Task<string> TranslateText(string text, string targetLanguage)
        {
            try
            {
                var requestBody = new
                {
                    text = new[] { text },
                    target_lang = targetLanguage
                };

                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("DeepL-Auth-Key", _authKey);

                var response = await _client.PostAsync(_apiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic translationResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    //extract the translated text
                    string translatedText = translationResponse.translations[0].text;

                    return translatedText;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new HttpRequestException("Translation request failed. Target language is not supported by DeepL.");
                    }
                    else
                    {
                        throw new HttpRequestException("Translation request failed.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Translation request failed.", ex);
            }
        }
    }
}

