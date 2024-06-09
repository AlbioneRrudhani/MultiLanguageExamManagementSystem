using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class TakenExam
    {
        public int TakenExamId { get; set; }
        public DateTime RequestTime { get; set; } //time when the student requested to take the exam
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; } 
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
