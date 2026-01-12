using FoodTracker.Services.DTO;
using FoodTracker.Models;

namespace FoodTracker.Views.ViewModels
{
    public sealed class DashboardVM
    {
        public MealStatsDTO statsDTO { get; init;  } = new();
        public IEnumerable<Meal> meals { get; init; } = Enumerable.Empty<Meal>();
        public DateTime? From { get; init; }
        public DateTime? To { get; init; }
    }
}
