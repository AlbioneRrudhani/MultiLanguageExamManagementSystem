using Mailjet.Client;
using MultiLanguageExamManagementSystem.Services.IServices;
using Newtonsoft.Json.Linq;
using Mailjet.Client.Resources;


namespace MultiLanguageExamManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["Mailjet:ApiKey"];
            _apiSecret = configuration["Mailjet:ApiSecret"];
        }

        public async Task SendExamResultsEmailAsync(string toEmail, int score, int correctAnswers, int totalQuestions)
        {
            var client = new MailjetClient(_apiKey, _apiSecret);

            var htmlContent = await GetEmailTemplateAsync("ExamResultsTemplate.html");
            htmlContent = string.Format(htmlContent, score, correctAnswers, totalQuestions);

            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "albionerr@gmail.com")
            .Property(Send.FromName, "Life From Gjirafa")
            .Property(Send.Subject, "Your Exam Results")
            .Property(Send.HtmlPart, htmlContent)
            .Property(Send.Recipients, new JArray {
            new JObject {
                { "Email", toEmail }
            }
            });

            await client.PostAsync(request);
        }

        private async Task<string> GetEmailTemplateAsync(string templateFileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Template", templateFileName);
            return await File.ReadAllTextAsync(path);
        }
    }
}