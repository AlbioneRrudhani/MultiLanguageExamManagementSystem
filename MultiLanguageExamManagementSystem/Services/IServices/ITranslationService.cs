namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ITranslationService
    {
        Task<string> TranslateText(string text, string targetLanguageCode);
    }
}
