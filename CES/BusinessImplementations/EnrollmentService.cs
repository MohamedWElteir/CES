using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class EnrollmentService(MyDbContext dbContext) : IEnrollmentService
{
    public async Task<(IEnumerable<Enrollment> Enrollments, int TotalPages)> GetPaginatedEnrollmentsAsync(int page, int pageSize)
    {
        var totalItems = await dbContext.Enrollments.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var enrollments = await dbContext.Enrollments
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();

        return (enrollments, totalPages);
    }


    public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid id)
    {
        return await dbContext.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentGuid == id);
    }

    public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
    {
        if (await IsCourseFullAsync(enrollment.CourseGuid))
        {
            throw new InvalidOperationException("Course is full");
        }

        if (await IsStudentEnrolledAsync(enrollment.CourseGuid, enrollment.StudentGuid))
        {
            throw new InvalidOperationException("Student is already enrolled in this course");
        }

        enrollment.EnrollmentGuid = Guid.NewGuid();
        dbContext.Enrollments.Add(enrollment);
        await dbContext.SaveChangesAsync();
        return enrollment;
    }

    public async Task DeleteEnrollmentAsync(Guid id)
    {
        var enrollment = await dbContext.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            dbContext.Enrollments.Remove(enrollment);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> IsCourseFullAsync(Guid courseId)
    {
        var course = await dbContext.Courses.FindAsync(courseId);
        if (course == null) return false;

        var enrollmentCount = await dbContext.Enrollments.CountAsync(e => e.CourseGuid == courseId);
        return enrollmentCount >= course.MaximumCapacity;
    }

    public async Task<bool> IsStudentEnrolledAsync(Guid courseId, Guid studentId)
    {
        return await dbContext.Enrollments.AnyAsync(e => e.CourseGuid == courseId && e.StudentGuid == studentId);
    }

    public async Task<int> GetAvailableSlotsAsync(Guid courseId)
    {
        var course = await dbContext.Courses.FindAsync(courseId);
        if (course == null) return 0;

        var enrollmentCount = await dbContext.Enrollments.CountAsync(e => e.CourseGuid == courseId);
        return course.MaximumCapacity - enrollmentCount;
    }

    public async Task<Dictionary<Guid, int>> GetEnrollmentCountsAsync()
    {
       return await dbContext.Enrollments
           .GroupBy(e => e.CourseGuid)
           .Select(g => new { CourseId = g.Key, Count = g.Count() })
           .ToDictionaryAsync(x => x.CourseId, x => x.Count);

    }
}