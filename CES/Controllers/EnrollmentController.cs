using CES.Models;
using CES.Services;
using Microsoft.AspNetCore.Mvc;

namespace CES.Controllers;

public class EnrollmentController(IEnrollmentService enrollmentService,
    IStudentService studentService,
    ICourseService courseService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var enrollments = await enrollmentService.GetAllEnrollmentsAsync();
        return View(enrollments);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Students = await studentService.GetAllStudentsAsync();
        ViewBag.Courses = await courseService.GetAllCoursesAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Enrollment enrollment)
    {
        if (!ModelState.IsValid) return View(enrollment);

        try
        {
            await enrollmentService.CreateEnrollmentAsync(enrollment);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.Students = await studentService.GetAllStudentsAsync();
            ViewBag.Courses = await courseService.GetAllCoursesAsync();
            return View(enrollment);
        }
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var enrollment = await enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null) return NotFound();

        return View(enrollment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await enrollmentService.DeleteEnrollmentAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableSlots(Guid courseId)
    {
        var availableSlots = await enrollmentService.GetAvailableSlotsAsync(courseId);
        return Json(new { availableSlots });
    }
}