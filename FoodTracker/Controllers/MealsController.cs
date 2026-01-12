using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodTracker.Data;
using FoodTracker.Models;
using NuGet.Protocol.Plugins;
using FoodTracker.Services;
using FoodTracker.Views.ViewModels;
using Humanizer;

namespace FoodTracker.Controllers
{
    public class MealsController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMealDashboardService _mealDashboardService;

        public MealsController(IMealService meals, IMealDashboardService dash)
        {
            _mealService = meals;
            _mealDashboardService = dash;
        }


        // GET: Meals
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var items = await _mealService.GetLastThreeMealsAsync(ct);
            return View(items);
        }


        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id, CancellationToken ct)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _mealService.GetMealByIdAsync(id.Value, ct);
            if (meal is null) return NotFound();

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meal, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(meal);

            await _mealService.CreateAsync(meal, ct);
            return RedirectToAction(nameof(Index));
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id, CancellationToken ct)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _mealService.GetMealByIdAsync(id.Value, ct);
            if (meal is null) return NotFound();
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Meal meal, CancellationToken ct)
        {
            if (id != meal.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return View(meal);

            var ok = await _mealService.UpdateAsync(meal, ct);
            if (!ok) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id, CancellationToken ct)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _mealService.GetMealByIdAsync(id.Value, ct);
            if (meal is null) return NotFound();

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            await _mealService.DeleteAsync(id, ct);
            return RedirectToAction(nameof(Index));
        }

        // Start Dashboard statistic requests here

        // Get: Meals/Dashboard
        [HttpGet]
        public async Task<IActionResult> Dashboard(DateTime? from, DateTime? to, CancellationToken ct)
        {
            // Ensure from/to are set to week range if not provided
            (from, to) = EnsureWeekRange(from, to);

            var stats = await _mealDashboardService.GetMealStatsAsync(from, to, ct);
            var allMeals = await _mealService.GetAllMealsAsync(ct);

            var vm = new DashboardVM
            {
                statsDTO = stats,
                meals = allMeals,
                From = from,
                To = to
            };

            return View(vm);
        }

        private static (DateTime from, DateTime to) EnsureWeekRange(DateTime? from, DateTime? to)
        {
            if (from is not null && to is not null)
            {
                return (from.Value.Date, to.Value.Date);
            }

            var today = DateTime.Today;
            var start = today.AddDays(-(int)today.DayOfWeek);
            var endExlusive = start.AddDays(7);

            return (start, endExlusive);

        }
    }
}
