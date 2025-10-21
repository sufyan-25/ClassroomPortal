using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.Models
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        [Required(ErrorMessage ="Title Required"), Display(Name = "Add Title")]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        public string title { get; set; }
        [Required(ErrorMessage = "Description Required"), Display(Name = "Add Description")]
        public string desc { get; set; }
        [Required(ErrorMessage ="Select the image")]
        [Display(Name = "Select Image")]
        public int GalleryID { get; set; }
        [ValidateNever]
        public virtual Gallery? Gallery { get; set; }
    }
    public class FAQ
    {
        [Key]
        public int FAQID { get; set; }
        [Required,Display(Name ="Question")]
        public string Question { get; set; }
        [Required, Display(Name = "Answer")]
        public string Answer { get; set; }
    }
    public class Contact
    {
        [Key]
        public int BranchID { get; set; }
        [Required(ErrorMessage = "Branch name is required"), Display(Name = "Branch")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Number is required"), Display(Name = "PTCL Number")]
        [RegularExpression(@"\+921\d{9}",ErrorMessage="+921-123456789 - Required")]
        public string PTCL { get; set; }
        [Required(ErrorMessage ="Number is required"), Display(Name = "Whatsapp Number")]
        [RegularExpression(@"\+923\d{9}",ErrorMessage ="+923-123456789 - Required" )]
        public string Whatsapp { get; set; }
        [Required(ErrorMessage = "Location is required"), Display(Name = "Location")]
        public string Location { get; set; }
        public string? thumb {  get; set; }
    }
}
