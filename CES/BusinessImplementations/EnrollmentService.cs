using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class EnrollmentService(MyDbContext context) : IEnrollmentService
{
    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid id)
    {
        return await context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentIdGuid == id);
    }

    public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
    {
        if (await IsCourseFullAsync(enrollment.CourseIdGuid))
        {
            throw new InvalidOperationException("Course is full");
        }

        if (await IsStudentEnrolledAsync(enrollment.CourseIdGuid, enrollment.StudentIdGuid))
        {
            throw new InvalidOperationException("Student is already enrolled in this course");
        }

        enrollment.EnrollmentIdGuid = Guid.NewGuid();
        context.Enrollments.Add(enrollment);
        await context.SaveChangesAsync();
        return enrollment;
    }

    public async Task DeleteEnrollmentAsync(Guid id)
    {
        var enrollment = await context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsCourseFullAsync(Guid courseId)
    {
        var course = await context.Courses.FindAsync(courseId);
        if (course == null) return false;

        var enrollmentCount = await context.Enrollments.CountAsync(e => e.CourseIdGuid == courseId);
        return enrollmentCount >= course.MaximumCapacity;
    }

    public async Task<bool> IsStudentEnrolledAsync(Guid courseId, Guid studentId)
    {
        return await context.Enrollments.AnyAsync(e => e.CourseIdGuid == courseId && e.StudentIdGuid == studentId);
    }

    public async Task<int> GetAvailableSlotsAsync(Guid courseId)
    {
        var course = await context.Courses.FindAsync(courseId);
        if (course == null) return 0;

        var enrollmentCount = await context.Enrollments.CountAsync(e => e.CourseIdGuid == courseId);
        return course.MaximumCapacity - enrollmentCount;
    }

    public async Task<Dictionary<Guid, int>> GetEnrollmentCountsAsync()
    {
       return await context.Enrollments
           .GroupBy(e => e.CourseIdGuid)
           .Select(g => new { CourseId = g.Key, Count = g.Count() })
           .ToDictionaryAsync(x => x.CourseId, x => x.Count);

    }
}