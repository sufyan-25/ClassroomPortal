using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.ViewModel
{
    public class AboutView
    {
        [Key]
        public int AboutID { get; set; }
        [Required,Display(Name ="Add Title")]
        [StringLength(maximumLength:20,MinimumLength =5)]
        public string title { get; set; }
        [Required,Display(Name ="Add Description")]
        public string desc { get; set; }
        [Required, Display(Name = "Add Image")]
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string? ShowImage { get; set; }
    }
}
