using System.Collections.Generic;

namespace CleanArch.Domain.Models
{
    public class Recipe{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
