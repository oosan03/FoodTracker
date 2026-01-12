using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [Required, StringLength(50)]
        public required string Name { get; set; }
        [Required]
        public RecipeType recipeType { get; set; }
        [StringLength(250)]
        public string? Instructions { get; set; }
    }

    public enum RecipeType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack,
        Dessert,
        Flexible
    }

}
