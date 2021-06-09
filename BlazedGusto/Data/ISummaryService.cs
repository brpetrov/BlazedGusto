using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazedGusto.Data
{
    public interface ISummaryService
    {
        event Action OnChange;

        Task AddToSummary(Ingredient ingredient, IEnumerable<Ingredient> ingredients, int Id, Meal meal);
        Task<List<Meal>> GetSummaryItems();
    }
}