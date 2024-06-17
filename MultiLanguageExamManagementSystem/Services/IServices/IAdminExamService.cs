using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface IAdminExamService
    {
        Task CreateExamAsync(ExamCreateDto examToCreate);
        Task CreateQuestionsAsync(List<QuestionCreateDto> questionsToCreate);
        Task AssociateQuestionsWithExamAsync(int examId, List<int> questionIds);
    }
}
