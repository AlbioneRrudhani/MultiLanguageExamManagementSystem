namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface IEmailService
    {
        Task SendExamResultsEmailAsync(string toEmail, int score, int correctAnswers, int totalQuestions);
    }
}
