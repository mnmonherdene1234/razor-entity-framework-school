using System.ComponentModel.DataAnnotations;

namespace razor_entity_framework.Models;

public class Student
{
    public int ID { get; set; }
    [Required]
    [MinLength(2)]
    public string? LastName { get; set; }
    [Required]
    [MinLength(2)]
    public string? FirstMidName { get; set; }
    [Required]
    public DateTime? EnrollmentDate { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
}