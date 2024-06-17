namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ?Auth0Id { get; set; }
    }
}
