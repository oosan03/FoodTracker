namespace FoodTracker.Services.DTO
{
    public sealed class MealStatsDTO
    {
        public int TotalMeals { get; init; }
        public int TotalCalories { get; init; }
        public decimal AveragePrice { get; init; }
        public decimal TotalSpent { get; init; }
        public int TotalProteins { get; init; }
        public int TotalHomemadeMeals { get; init; }
        public int TotalEatoutMeals { get; init; }


    }
}
