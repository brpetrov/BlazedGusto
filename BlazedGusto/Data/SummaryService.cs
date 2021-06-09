using Blazored.LocalStorage;
using Blazored.Toast.Services;
using DataLibrary.Models;
using DataLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazedGusto.Data
{
    public class SummaryService : ISummaryService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IMealService _mealService;

        public event Action OnChange;

        public SummaryService(ILocalStorageService localStorage, IToastService toastService,
            IMealService mealService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _mealService = mealService;
        }

        public async Task AddToSummary(Ingredient ingredient, IEnumerable<Ingredient> ingredients, int Id, Meal meal)
        {

            var summary = await _localStorage.GetItemAsync<List<Ingredient>>("summary");
            if (summary == null)
            {
                summary = new List<Ingredient>();
            }
            foreach (var ingr in ingredients)
            {
                if (ingr.Meal.MealId == Id)
                {
                    summary.Add(ingr);
                    await _localStorage.SetItemAsync("summary", summary);

                }
            }
           ;


            _toastService.ShowSuccess(meal.Title, "Added to cart.");

            OnChange.Invoke();
        }

        public async Task<List<Meal>> GetSummaryItems()
        {
            var result = new List<Meal>();
            var summary = await _localStorage.GetItemAsync<List<Meal>>("summary");
            if (summary == null)
            {
                return result;
            }

            foreach (var item in summary)
            {
                var meal = await _mealService.Meal_GetOne(item.MealId);
            }

            result = summary;
            return result;

        }
    }
}
