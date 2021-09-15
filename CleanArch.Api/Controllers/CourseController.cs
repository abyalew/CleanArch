using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        public Response<CourseViewModel> GetCourses()
        { 
            return new Response<CourseViewModel>(HttpStatusCode.OK) {Content = _courseService.GetCourses() };
        }
    }
}
