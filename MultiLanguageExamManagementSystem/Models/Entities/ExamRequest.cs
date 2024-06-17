using MultiLanguageExamManagementSystem.Models.Enum;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class ExamRequest
    {
        public int ExamRequestId { get; set; }

        //request-related fields
        public DateTime RequestTime { get; set; }

        public RequestStatus Status { get; set; }

        public int AttemptCount { get; set; }

        public string UserId { get; set; }


        //approval-related fields
        public DateTime? ApprovalTime { get; set; }
        public string? ProfessorId { get; set; } 


        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
