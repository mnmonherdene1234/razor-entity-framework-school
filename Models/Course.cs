using System.ComponentModel.DataAnnotations.Schema;

namespace razor_entity_framework.Models;

public class Course
{
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}