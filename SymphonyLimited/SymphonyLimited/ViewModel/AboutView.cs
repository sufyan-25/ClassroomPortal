using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SymphonyLimited.Models;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.ViewModel
{
    public class TopicView
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Select Course"), Display(Name = "Select Course")]
        public int CourseID { get; set; }
        [ValidateNever]
        public virtual Course? Courses { get; set; }

        [Required(ErrorMessage = "Topic name required"), Display(Name = "Topic Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Topic description required"), Display(Name = "Topic Description")]
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

}
