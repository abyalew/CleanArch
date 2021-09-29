using CleanArch.Application.Abstractions.Dtos;
using System.Collections.Generic;

namespace CleanArch.Application.Abstractions.Interfaces
{
    public interface ICourseService
    {
        List<CourseDto> GetCourses();
        CourseDto Add(CourseDto course);
    }
}
