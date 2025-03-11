using CES.Models;
using CES.Services;
using Microsoft.AspNetCore.Mvc;

namespace CES.Controllers;

public class StudentController(IStudentService studentService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var students = await studentService.GetAllStudentsAsync();
        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid) return View(student);

        if (await studentService.EmailExistsAsync(student.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(student);
        }
        await studentService.CreateStudentAsync(student);
        return RedirectToAction(nameof(Index));
    }

}