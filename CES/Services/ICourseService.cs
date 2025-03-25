using CES.Models;

namespace CES.Services;

public interface ICourseService
{
    Task<(IEnumerable<Course> Courses, int TotalPages)> GetPaginatedCoursesAsync(int page, int pageSize);

    [Obsolete("Use GetPaginatedCoursesAsync instead")]
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(Guid id);
    Task<Course> CreateCourseAsync(Course course);
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Guid id);
}