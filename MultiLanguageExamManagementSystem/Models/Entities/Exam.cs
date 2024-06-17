using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public string ProfessorId { get; set; }
        public ICollection<Exam_Question> Exam_Questions { get; set; }
    }
}
