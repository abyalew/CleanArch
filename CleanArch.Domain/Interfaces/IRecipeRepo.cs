using System.Collections.Generic;
using CleanArch.Domain.Models;

namespace CleanArch.Domain.Interfaces
{
    public interface IRecipeRepo:IRepo<Recipe>{
        IEnumerable<Recipe> GetRecipesWithIngredients();
    }
}
