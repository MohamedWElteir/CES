using CES.Models;

namespace CES.Services;

public interface IStudentService
{
    Task<(IEnumerable<Student> Students, int TotalPages)> GetPaginatedStudentsAsync(int page, int pageSize);

    [Obsolete("Use GetPaginatedStudentsAsync instead")]
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(Guid id);
    Task<Student> CreateStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(Guid id);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NationalIdExistsAsync(string studentNationalId);

}