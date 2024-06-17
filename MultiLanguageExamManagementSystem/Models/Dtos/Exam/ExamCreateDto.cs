using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Dtos.Exam
{
    public class ExamCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
