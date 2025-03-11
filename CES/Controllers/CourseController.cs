using CES.Models;
using CES.Services;
using Microsoft.AspNetCore.Mvc;

namespace CES.Controllers;

public class CourseController(ICourseService courseService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var courses = await courseService.GetAllCoursesAsync();
        return View(courses);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid) return View(course);

        await courseService.CreateCourseAsync(course);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var course = await courseService.GetCourseByIdAsync(id);
        if (course == null) return NotFound();

        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Course course)
    {
        if (!ModelState.IsValid) return View(course);

        await courseService.UpdateCourseAsync(course);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var course = await courseService.GetCourseByIdAsync(id);
        if (course == null) return NotFound();

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await courseService.DeleteCourseAsync(id);
        return RedirectToAction(nameof(Index));
    }
}