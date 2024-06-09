using MultiLanguageExamManagementSystem.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(320)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }


        public UserRole? Role { get; set; } //if the role is null, the user is cosidered a student
    }
}
