using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Dtos.Question
{
    public class QuestionCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Text { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }
    }
}
