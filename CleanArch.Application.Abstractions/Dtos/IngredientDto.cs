using AutoMapper;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Abstractions.Dtos
{
    [AutoMap(typeof(Ingredient),ReverseMap = true)]
    public class IngredientDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}