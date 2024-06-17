using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Language
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string LanguageCode { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }


        //Optional
        public bool IsDefault { get; set; }
        public bool IsSelected { get; set; }

        public ICollection<LocalizationResource> LocalizationResources { get; set; }


        #region requirements
        // Language will have Id, Name, LanguageCode, CountryId (IsDefault and IsSelected are optional properties)
        #endregion

    }
}
