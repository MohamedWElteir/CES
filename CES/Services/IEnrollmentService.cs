using CES.Models;

namespace CES.Services;

public interface IEnrollmentService
{
    Task<(IEnumerable<Enrollment> Enrollments, int TotalPages)> GetPaginatedEnrollmentsAsync(int page, int pageSize);

    Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
    Task<Enrollment?> GetEnrollmentByIdAsync(Guid id);
    Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);
    Task DeleteEnrollmentAsync(Guid id);
    Task<bool> IsCourseFullAsync(Guid courseId);
    Task<bool> IsStudentEnrolledAsync(Guid courseId, Guid studentId);
    Task<int> GetAvailableSlotsAsync(Guid courseId);
    Task<Dictionary<Guid, int>> GetEnrollmentCountsAsync();
}