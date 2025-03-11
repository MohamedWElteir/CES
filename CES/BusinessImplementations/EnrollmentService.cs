using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class EnrollmentService(MyDbContext context) : IEnrollmentService
{
    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await context.Enrollments.ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid id)
    {
        return await context.Enrollments.FindAsync(id);
    }

    public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
    {
        if (await IsCourseFullAsync(enrollment.CourseIdGuid) || await IsStudentEnrolledAsync(enrollment.CourseIdGuid, enrollment.StudentIdGuid))
        {
            throw new InvalidOperationException("Cannot enroll student.");
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
}