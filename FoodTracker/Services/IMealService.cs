using FoodTracker.Models;

namespace FoodTracker.Services
{
    public interface IMealService
    {
        Task<List<Meal>> GetAllMealsAsync(CancellationToken ct = default);
        Task<Meal?> GetMealByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(Meal meal, CancellationToken ct = default);
        Task<bool> UpdateAsync(Meal meal, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);

        Task<List<Meal>> GetThisWeeksMealsAsync(CancellationToken ct = default);
    } 
}
