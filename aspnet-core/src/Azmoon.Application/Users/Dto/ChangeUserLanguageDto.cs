using System.ComponentModel.DataAnnotations;

namespace Azmoon.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}