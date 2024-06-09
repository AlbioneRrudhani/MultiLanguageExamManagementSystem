using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string CorrectAnswer { get; set; }
       
        public ICollection<Exam_Question> Exam_Questions { get; set; }
    }
}
