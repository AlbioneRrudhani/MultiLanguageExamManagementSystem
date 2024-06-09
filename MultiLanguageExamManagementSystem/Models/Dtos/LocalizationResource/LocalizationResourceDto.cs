namespace MultiLanguageExamManagementSystem.Models.Dtos.LocalizationResource
{
    public class LocalizationResourceDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Namespace { get; set; }
        public string BeautifiedNamespace { get; set; }
        public int LanguageId { get; set; }
    }
}
