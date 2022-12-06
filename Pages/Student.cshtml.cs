using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_entity_framework.Data;
using razor_entity_framework.Models;

namespace razor_entity_framework.Pages;

public class StudentModel : PageModel
{
    private readonly SchoolContext _context;

    public StudentModel(SchoolContext schoolContext)
    {
        _context = schoolContext;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var studentToUpdate = await _context.Students.FindAsync(id);

        if (studentToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Student>(
            studentToUpdate,
            "student",
            s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreate()
    {
        Student student = new Student()
        {
            FirstMidName = Request.Form["first_name"],
            LastName = Request.Form["last_name"]
        };

        await _context.Students.AddAsync(student);

        await _context.SaveChangesAsync();

        return new OkResult();
    }

    public List<Student> GetStudents()
    {
        return _context.Students.ToList();
    }
}