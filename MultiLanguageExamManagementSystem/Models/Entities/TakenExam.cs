namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class TakenExam
    {
        public int TakenExamId { get; set; }
        public bool IsCompleted { get; set; }

        public string UserId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

    }
}
