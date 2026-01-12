using FoodTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Services
{
    public class MealDashboardService: IMealDashboardService
    {
        private readonly AppDbContext _db;

        public MealDashboardService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<DTO.MealStatsDTO> GetMealStatsAsync(DateTime? from, DateTime? to, CancellationToken ct = default)
        {
            var q = _db.Meals.AsNoTracking().AsQueryable();

            if (from is not null) q = q.Where(m => m.DateConsumed >= from.Value);
            if (to is not null) q = q.Where(m => m.DateConsumed <= to.Value);

            var agg = await q.GroupBy(m => 1).Select(g => new
            {
                TotalMeals = g.Count(),
                TotalCalories = g.Sum(m => m.Calories),
                TotalProtein = g.Sum(m => m.Proteins),
                AveragePrice = g.Average(m => m.Price),
                TotalSpent = g.Sum(m => m.Price),
                TotalHomemadeMeals = g.Count(m => m.Homemade),
                TotalEatoutMeals = g.Count(m => !m.Homemade),
            }).SingleOrDefaultAsync(ct);

            if (agg is null)
            {
                return new DTO.MealStatsDTO();
            }

            return new DTO.MealStatsDTO
            {
                TotalMeals = agg.TotalMeals,
                TotalCalories = agg.TotalCalories,
                TotalProteins = agg.TotalProtein,
                AveragePrice = agg.AveragePrice,
                TotalSpent = agg.TotalSpent,
                TotalHomemadeMeals = agg.TotalHomemadeMeals,
                TotalEatoutMeals = agg.TotalEatoutMeals,

            };
        }

        

    }
}
