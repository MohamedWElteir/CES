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

        if (await studentService.EmailExistsAsync(student.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
        }

        if(await studentService.NationalIdExistsAsync(student.NationalId))
        {
            ModelState.AddModelError("NationalId", "National ID already exists");
        }

        if (!ModelState.IsValid) return View(student);

        await studentService.CreateStudentAsync(student);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await studentService.GetStudentByIdAsync(id);
        if (student is null) return NotFound();

        return View(student);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await studentService.GetStudentByIdAsync(id);
        if (student is null) return NotFound();

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await studentService.DeleteStudentAsync(id);
        return RedirectToAction(nameof(Index));
    }

}