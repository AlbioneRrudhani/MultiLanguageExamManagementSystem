namespace MultiLanguageExamManagementSystem.Models.Dtos.Exam
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ProfessorId { get; set; } 
    }
}
