namespace CleanArch.Domain.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}