using CleanArch.Application.Abstractions.Dtos;
using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using System.Collections.Generic;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAutoMap _autoMap;

        public CourseService(ICourseRepository courseRepository, IAutoMap autoMap)
        {
            _courseRepository = courseRepository;
            _autoMap = autoMap;
        }

        public List<CourseDto> GetCourses()
        {
            return _autoMap.MapTo<IEnumerable<Course>, List<CourseDto>>(_courseRepository.GetCourses());
        }

        public CourseDto Add(CourseDto course)
        {
            var newCourse = _courseRepository.Add(_autoMap.MapTo<CourseDto,Course>(course));
            return _autoMap.MapTo<Course, CourseDto>(newCourse);
        }
    }
}
