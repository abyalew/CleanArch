using CleanArch.Application.Abstractions.Dtos;
using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace CleanArch.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repo;
        private readonly IAutoMap _autoMap;

        public RecipeService(IRecipeRepo repo,IAutoMap autoMap)
        {
            _repo = repo;
            _autoMap = autoMap;
        }

        public IEnumerable<RecipeDto> GetRecipes()
        {
            var recipes = _repo.GetRecipesWithIngredients();
            return _autoMap.MapTo<IEnumerable<Recipe>,IEnumerable<RecipeDto>>(recipes);
        }

        public RecipeDto GetById(int id)
        {
            var recipe = _repo.Find(r=>r.Id == id,r=>r.Ingredients).FirstOrDefault();
            return _autoMap.MapTo<Recipe,RecipeDto>(recipe);
        }
        
        public RecipeDto Add(RecipeDto recipe)
        {
            var newRecipe = _repo.Add(_autoMap.MapTo<RecipeDto,Recipe>(recipe));
            return _autoMap.MapTo<Recipe,RecipeDto>(newRecipe);
        }

        public RecipeDto Delete(RecipeDto recipe)
        {
            
            var newRecipe = _repo.Delete(_autoMap.MapTo<RecipeDto,Recipe>(recipe));
            return _autoMap.MapTo<Recipe,RecipeDto>(newRecipe);
        }

        public RecipeDto Update(RecipeDto recipe)
        {
            var newRecipe = _repo.Update(_autoMap.MapTo<RecipeDto,Recipe>(recipe));
            return _autoMap.MapTo<Recipe,RecipeDto>(newRecipe);
        }
    }
}
