﻿using CES.DbContext;
using CES.Models;
using CES.Services;
using Microsoft.EntityFrameworkCore;

namespace CES.BusinessImplementations;

public class CourseService(MyDbContext dbContext) : ICourseService
{
    public async Task<(IEnumerable<Course> Courses, int TotalPages)> GetPaginatedCoursesAsync(int page, int pageSize)
    {
        var totalItems = await dbContext.Courses.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var courses = await dbContext.Courses
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (courses, totalPages);
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
       return await dbContext.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await dbContext.Courses.FindAsync(id);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        course.CourseIdGuid = Guid.NewGuid();
        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();
        return course;
    }

    public async Task UpdateCourseAsync(Course course)
    {
        dbContext.Entry(course).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        var course = await dbContext.Courses.FindAsync(id);
        if (course != null)
        {
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
        }
    }

}