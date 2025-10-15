using SymphonyLimited.Models;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.ViewModel
{
    public class StudentView
    {
        [Required(ErrorMessage = "Name is requierd"), Display(Name = "Student Name")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is requierd"), Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Number is requierd"), Display(Name = "Mobile Number")]
        [RegularExpression(@"\+923\d{9}", ErrorMessage = "Number format is (+9231XXXXXXX9) is not valid")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address Required"), Display(Name = "Address")]
        [StringLength(30)]
        public string Address { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
