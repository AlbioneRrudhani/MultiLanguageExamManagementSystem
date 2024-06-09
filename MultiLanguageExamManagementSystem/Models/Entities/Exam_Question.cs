namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Exam_Question
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
