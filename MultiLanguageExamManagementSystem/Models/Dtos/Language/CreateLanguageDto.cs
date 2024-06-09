namespace MultiLanguageExamManagementSystem.Models.Dtos.Language
{
    public class CreateLanguageDto
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSelected { get; set; }
        public int CountryId { get; set; }
    }
}
