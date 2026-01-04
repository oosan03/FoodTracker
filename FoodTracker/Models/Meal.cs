using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(0, 50000)]
        public int Calories { get; set; }
        [Range(0, 50000)]
        public int Fats { get; set; }
        [Range(0, 50000)]
        public int Carbohydrates { get; set; }
        [Range(0, 50000)]
        public int Proteins { get; set; }
        [Range(0, 50000)]
        public int Sugars { get; set; }
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        [Range(0, 10000)]
        public decimal Price { get; set; }
        public bool Homemade { get; set; }


    }
}
