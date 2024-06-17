namespace MultiLanguageExamManagementSystem.Models.Dtos.ExamSubmission
{
    public class SubmitExamDto
    {
        public int ExamId { get; set; }
        public Dictionary<int, string> Answers { get; set; }
    }
}
