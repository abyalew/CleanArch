using CleanArch.Application.Abstractions.Dtos;
using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Domain.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.IntegrationTest.Services
{
    using static Testing;
    public class CourseTest: TestBase
    {
        private readonly ICourseService _courseService;

        public CourseTest()
        {
            _courseService = GetService<ICourseService>();
        }

        [Test]
        public void AddTest()
        {
            var course = new CourseDto
            {
                Name = "My Course",
                Description = "My Course",
                ImageUrl = "no image",
            };

            var addedCourse = _courseService.Add(course);

            addedCourse.Should().NotBeNull();
            addedCourse.Name.Should().Be(course.Name);
            addedCourse.Description.Should().Be(course.Description);
            addedCourse.Id.Should().BeGreaterThan(0);
        }
    }
}
