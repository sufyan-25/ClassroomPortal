using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.ViewModel
{
    public class GalleryView
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="Text Required"),Display(Name ="Image Alter Text")]
        public string AltText { get; set; } = "Default Image";
        [Required(ErrorMessage="Image Required"),Display(Name ="Select Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
