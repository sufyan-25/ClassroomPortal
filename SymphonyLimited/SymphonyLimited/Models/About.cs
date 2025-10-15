using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.Models
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string desc { get; set; }
        [Required]
        public string base64 { get; set; }
        [Required]
        public string thumb { get; set; }
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
