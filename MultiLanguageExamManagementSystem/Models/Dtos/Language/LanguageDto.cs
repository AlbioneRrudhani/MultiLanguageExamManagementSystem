namespace MultiLanguageExamManagementSystem.Models.Dtos.Language
{
    public class LanguageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public int CountryId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSelected { get; set; }


    }
}
