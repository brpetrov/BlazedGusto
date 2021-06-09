using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Services
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> MealDateRange(DateTime StartDate, DateTime EndDate);
        Task<bool> MealDelete(int MealId);
        Task<bool> MealInsert(Meal meal);
        Task<IEnumerable<Meal>> MealList();
        Task<IEnumerable<Meal>> MealSearch(string Param);
        Task<bool> MealUpdate(Meal meal);
        Task<Meal> Meal_GetOne(int MealId);
    }
}