using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string CorrectAnswer { get; set; }

        public string ProfessorId { get; set; }
        public ICollection<Exam_Question> Exam_Questions { get; set; }
    }
}
