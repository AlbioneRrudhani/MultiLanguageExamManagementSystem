using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsVisible { get; set; }


        public int ProfessorId { get; set; }
        public User Professor { get; set; }

        public ICollection<Exam_Question> Exam_Questions { get; set; }
    }
}
