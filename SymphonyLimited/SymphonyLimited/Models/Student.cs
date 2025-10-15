using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SymphonyLimited.Models
{
    public class Student
    {
        [Key]
        [StringLength(10)]
        public string StudentID { get; set; }
        [Required(ErrorMessage = "Name is requierd"), Display(Name ="Student Name")]
        [StringLength(20, MinimumLength =3)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email is requierd"), Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Number is requierd"), Display(Name = "Mobile Number")]
        [RegularExpression(@"\+923\d{9}",ErrorMessage = "Number format is (+9231XXXXXXX9) is not valid")]
        public string Phone { get; set; }
        [Required(ErrorMessage="Address Required"),Display(Name="Address")]
        [StringLength(30)]
        public string Address { get; set; }
        public string ThumbPic {  get; set; }
        [Required(ErrorMessage="Status Required (Entrance or Enrolled"),Display(Name="Status")]
        [StringLength(10)]
        public string Status{ get; set; }

        public virtual ICollection<Result>? Results { get; set; }
    }
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is requierd"), Display(Name = "Exam Title")]
        [StringLength(20)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Date is requierd"), Display(Name = "Exam Date")]
        public String ExamDate { get; set; }
        [Required(ErrorMessage = "Time is requierd"), Display(Name = "Exam Time")]
        public String ExamTime { get; set; }
        [Required(ErrorMessage = "Fee is requierd"), Display(Name = "Exam Fee")]
        [Range(1, double.MaxValue)]
        public int Fee { get; set; }
        [Required(ErrorMessage = "Description is requierd"), Display(Name = "Description")]
        public string Desc { get; set; }
        public virtual ICollection<Result>? Results { get; set; }
    }
    public class Result
    {
        [Key]
        public int Id { get; set; }
        //Foreign Key
        public string StudentID {  get; set; }
        [ValidateNever]
        public virtual Student? Student { get; set; }
        public int ExamID { get; set; }
        [ValidateNever]
        public virtual Exam? Exam { get; set; }
        [Required(ErrorMessage ="Marks required"),Display(Name ="Students Marks")]
        public int Marks { get; set; }
        [Display(Name ="Status of Student")]
        public string Status { get; set; }
    }
}
