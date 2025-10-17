using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name required"),Display(Name ="Course Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Duration of course required"),Display(Name ="Course Duration")]
        [StringLength(20)]
        public string Duration { get; set; }
        [Required(ErrorMessage ="Fee of course required"),Display(Name ="Course Fee")]
        public int Fee { get; set; }
        [Required(ErrorMessage ="Description of course required"),Display(Name ="Course Description")]
        public string Description { get; set; }
        [ValidateNever]
        public virtual ICollection<Topic>? Topics { get; set; } 
    }
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Select Course"),Display(Name ="Select Course")]
        public int CourseID { get; set; }
        [ValidateNever]
        public virtual Course? Courses { get; set; }

        [Required(ErrorMessage ="Topic name required"),Display(Name ="Topic Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Topic description required"),Display(Name ="Topic Description")]
        public string Description { get; set; }
        public string Image {  get; set; }
    }
}
