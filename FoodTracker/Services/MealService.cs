using FoodTracker.Data;
using System.Linq; 

using Microsoft.EntityFrameworkCore;
using FoodTracker.Models;

namespace FoodTracker.Services
{
    public sealed class MealService: IMealService
    {
        private readonly AppDbContext _db;

        public MealService(AppDbContext db)
        {
            _db = db;
        }

        public Task<List<Meal>> GetAllMealsAsync(CancellationToken ct = default)
        {
            return _db.Meals
                .AsNoTracking()
                .Include(m => m.Recipe)
                .OrderByDescending(m => m.Id)
                .ToListAsync(ct);
        }

        public Task<List<Meal>> GetLastThreeMealsAsync(CancellationToken ct = default)
        {
            return _db.Meals
                .AsNoTracking()
                .OrderByDescending(m => m.Id)
                .Take(3)
                .ToListAsync(ct);
        }

        public Task<Meal?> GetMealByIdAsync(int id, CancellationToken ct = default)
        {
            return _db.Meals
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task<int> CreateAsync(Meal meal, CancellationToken ct = default)
        {
            _db.Meals.Add(meal);
            await _db.SaveChangesAsync(ct);
            return meal.Id;
        }

        public async Task<bool> UpdateAsync(Meal meal, CancellationToken ct = default) 
        {
            var exists = await _db.Meals.AnyAsync(m => m.Id == meal.Id, ct);
            if (!exists) return false;

            _db.Meals.Update(meal);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct= default)
        {
            var entity = await _db.Meals.FirstOrDefaultAsync(m => m.Id == id, ct);
            if (entity is null) return false;

            _db.Meals.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
        {
            return _db.Meals.AnyAsync(m => m.Id == id, ct);
        }

        // Additional methods to support dashboard statistics

        public Task<List<Meal>> GetThisWeeksMealsAsync(CancellationToken ct = default)
        {
            var weekStartsOn = DayOfWeek.Monday;
            var now = DateTime.Now;
            int diff = (7 + (now.DayOfWeek - weekStartsOn)) % 7;
            var startOfWeek = now.Date.AddDays(-diff);
            var startOfNextWeek = startOfWeek.AddDays(7);

            return _db.Meals
                .AsNoTracking()
                .Where(m => m.DateConsumed >= startOfWeek && m.DateConsumed < startOfNextWeek)
                .ToListAsync(ct);
        }
    }
}
