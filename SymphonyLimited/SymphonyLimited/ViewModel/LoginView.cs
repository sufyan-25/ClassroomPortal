using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.ViewModel
{
    public class LoginView
    {
        [Required(ErrorMessage ="Email required"),Display(Name ="Username")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password required"), Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(15,MinimumLength =8,ErrorMessage ="Password should be minimum 6 characters & maximum 15")]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
