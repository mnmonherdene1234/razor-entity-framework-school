using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_entity_framework.Data;
using razor_entity_framework.Models;

namespace razor_entity_framework.Pages;

public class StudentsModel : PageModel
{
    private readonly SchoolContext _context;
    [BindProperty]
    public Student Student { get; set; }
    public List<Student> Students { get; set; }
    public StudentsModel(SchoolContext schoolContext)
    {
        _context = schoolContext;
        Students = _context.Students.ToList();
        Student = new()
        {
            ID = 0,
            FirstMidName = "",
            LastName = "",
            EnrollmentDate = DateTime.Now
        };
    }

    public void OnGet()
    {
        if (Request.Query["id"].ToString() != "")
        {
            int id = Convert.ToInt32(Request.Query["id"].ToString());
            Student? stutent = _context.Students.Where(s => s.ID == id).FirstOrDefault();
            if (stutent != null)
            {
                Student = stutent;
            }
        }
    }

    public void OnPostAdd()
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(Student);
            _context.SaveChanges();
            Students = _context.Students.ToList();
        }
    }

    public void OnPostEdit(int? id)
    {
        if (id != null)
        {
            if (ModelState.IsValid)
            {
                Student? student = _context.Students.Where(s => s.ID == id).FirstOrDefault();
                if (student != null)
                {
                    student.FirstMidName = Student.FirstMidName;
                    student.LastName = Student.LastName;
                    student.EnrollmentDate = Student.EnrollmentDate;
                    _context.SaveChanges();
                    Students = _context.Students.ToList();
                }
            }
        }
    }

    public void OnPostDelete(int? id)
    {
        if (id != null)
        {
            Student? student = _context.Students.Where(s => s.ID == id).FirstOrDefault();
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                Students = _context.Students.ToList();
            }
        }
    }

    public void OnGetView(int? id)
    {
        if (id != null)
        {
            Student? student = _context.Students.Where(s => s.ID == id).FirstOrDefault();
            if (student != null)
            {
                Student = student;
            }
        }
    }
}