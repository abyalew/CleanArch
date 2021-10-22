using CleanArch.Application.Abstractions.Dtos;
using System.Collections.Generic;

namespace CleanArch.Application.Abstractions.Interfaces
{
    public interface IRecipeService{
        IEnumerable<RecipeDto> GetRecipes();
        RecipeDto GetById(int id);
        RecipeDto Add(RecipeDto recipe);
        RecipeDto Update(RecipeDto recipe);
        RecipeDto Delete(RecipeDto recipe);
    }
}
