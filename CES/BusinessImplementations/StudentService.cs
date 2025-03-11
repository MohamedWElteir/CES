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
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            student.StudentIdGuid = Guid.NewGuid();
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return student;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }


    }

    public async Task UpdateStudentAsync(Student student)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            dbContext.Entry(student).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteStudentAsync(Guid id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        var student = await dbContext.Students.FindAsync(id);
        if (student != null)
        {
            try
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await dbContext.Students.AnyAsync(s => s.Email == email);
    }
}