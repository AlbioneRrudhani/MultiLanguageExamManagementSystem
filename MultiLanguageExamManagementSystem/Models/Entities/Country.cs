using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        public ICollection<Language> Languages { get; set; }


        #region requirements
        // Caountry will have Id, Name and Code
        #endregion

    }
}
