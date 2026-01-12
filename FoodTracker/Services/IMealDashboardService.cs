namespace FoodTracker.Services
{
    public interface IMealDashboardService
    {
        Task<DTO.MealStatsDTO> GetMealStatsAsync(DateTime? from, DateTime? to, CancellationToken ct = default);
    }
}
