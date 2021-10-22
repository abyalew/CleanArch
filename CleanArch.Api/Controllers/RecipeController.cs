using Microsoft.AspNetCore.Mvc;
using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Auth;
using System.Collections.Generic;
using CleanArch.Application.Abstractions.Dtos;
using System.Net;
using System.Linq;

namespace CleanArch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        public List<RecipeDto> GetRecipes()
        {
            return _recipeService.GetRecipes().ToList();
        }
        [HttpGet]
        public RecipeDto GetById(int id)
        {
            return _recipeService.GetById(id);
        }
        [HttpPost]
        public RecipeDto Add(RecipeDto recipe)
        {
            return _recipeService.Add(recipe);
        }
        [HttpPost]
        public RecipeDto Update(RecipeDto recipe)
        {
            return _recipeService.Update(recipe);
        }
        [HttpPost]
        public RecipeDto Delete(RecipeDto recipe)
        {
            return _recipeService.Delete(recipe);
        }
    }
}
