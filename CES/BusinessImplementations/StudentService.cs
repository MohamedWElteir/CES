using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class StudentService(MyDbContext dbContext) : IStudentService
{
    public async Task<(IEnumerable<Student> Students, int TotalPages)> GetPaginatedStudentsAsync(int page, int pageSize)
    {
        var totalItems = await dbContext.Students.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var students = await dbContext.Students
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (students, totalPages);
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await dbContext.Students.ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(Guid id)
    {
       return await dbContext.Students.FindAsync(id);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {

            student.StudentGuid = Guid.NewGuid();
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return student;

    }

    public async Task UpdateStudentAsync(Student student)
    {
            dbContext.Entry(student).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(Guid id)
    {
        var student = await dbContext.Students
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.StudentGuid == id);
        if (student != null)
        {
            dbContext.Enrollments.RemoveRange(student.Enrollments);
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await dbContext.Students.AnyAsync(s => s.Email == email);
    }

    public async Task<bool> NationalIdExistsAsync(string studentNationalId)
    {
        return await dbContext.Students.AnyAsync(s => s.NationalId == studentNationalId);
    }
}