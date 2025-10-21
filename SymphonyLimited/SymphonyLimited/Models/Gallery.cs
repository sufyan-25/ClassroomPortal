using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        public string AltText { get; set; }
        public string ImageBs64 { get; set; }
        public string Thumb64 { get; set; }
        [ValidateNever]
        public virtual ICollection<About>? Abouts { get; set; }
        [ValidateNever]
        public virtual ICollection<Topic>? Topics { get; set; }
        [ValidateNever]
        public virtual ICollection<Course>? Courses { get; set; }
        [ValidateNever]
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}