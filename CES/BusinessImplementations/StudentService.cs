using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class StudentService(MyDbContext dbContext) : IStudentService
{
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

            student.StudentIdGuid = Guid.NewGuid();
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
        var student = await dbContext.Students.FindAsync(id);
        if (student != null)
        {
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