using System.ComponentModel.DataAnnotations;

namespace DasignoTest.DTOs.userDTOs
{
    public record UpdateUserDTO
    {
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Name can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        public string? name { get; set; }
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Second Name can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        public string? secondName { get; set; }
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the SurName can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        public string? surName { get; set; }
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Second SurName can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        public string? secondSurName { get; set; }
        
        public DateTime? birthDate { get; set; }
        
        public int? salary { get; set; }
    }
}
