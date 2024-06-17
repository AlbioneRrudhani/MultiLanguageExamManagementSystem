using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.ExamSubmission;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetExams();
        Task RequestToTakeExamAsync(int examId);
        Task<List<ExamDto>> GetApprovedExamsForUserAsync();
        Task<List<QuestionDto>> GetExamQuestionsAsync(int examId);

    }
}
