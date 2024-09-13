using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DasignoTest.DTOs.userDTOs
{
    public record CreateUserDTO
    {
        [Required(ErrorMessage = "the Name is required")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Name can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        [DefaultValue("")]
        public string name { get; set; }
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Second Name can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        [DefaultValue("")]
        public string? secondName { get; set; } = "";
        [Required(ErrorMessage = "the Surname is required")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the SurName can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        [DefaultValue("")]
        public string surName { get; set; } = "";
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]{1,50}$", ErrorMessage = "the Second SurName can't have numbers, special character or spaces and it can have a maximun extension of 50 characters")]
        [DefaultValue("")]
        public string? secondSurName { get; set; } = "";
        [Required(ErrorMessage = "the BirthDate is required")]
        
        public DateTime birthDate { get; set; } 
        [Required(ErrorMessage = "the Salary is required")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "the Salary  can't be 0 or a negative number")]
        
        public int salary { get; set; }
    }
}
