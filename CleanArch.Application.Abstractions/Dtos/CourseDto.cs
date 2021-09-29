﻿using AutoMapper;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Abstractions.Dtos
{
    [AutoMap(typeof(Course),ReverseMap = true)]
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
