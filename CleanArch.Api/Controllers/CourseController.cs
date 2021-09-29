using CleanArch.Application.Abstractions.Dtos;
using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace CleanArch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize]
        public Response<List<CourseDto>> GetCourses()
        {
            return new Response<List<CourseDto>>(HttpStatusCode.OK) { Content = _courseService.GetCourses() };
        }

        [HttpGet]
        [Authorize]
        public Response<CourseDto> Add([FromBody]CourseDto course)
        {
            return new Response<CourseDto>(HttpStatusCode.OK) { Content = _courseService.Add(course) };
        }
    }
}
