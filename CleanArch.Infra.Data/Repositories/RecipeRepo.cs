using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CleanArch.Infra.Data.Repositories
{
    public class RecipeRepo : Repo<Recipe>, IRecipeRepo
    {
        private new readonly UniversityDbContext _context;
        public RecipeRepo(UniversityDbContext context):base(context)
        {
            _context = context;
        }
        public IEnumerable<Recipe> GetRecipesWithIngredients()
        {
            return _context.Recipes.Include(r=>r.Ingredients);
        }
    }
}
