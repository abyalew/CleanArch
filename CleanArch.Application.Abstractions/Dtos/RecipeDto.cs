using System.Collections.Generic;
using AutoMapper;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Abstractions.Dtos
{
    [AutoMap(typeof(Recipe),ReverseMap = true)]
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
