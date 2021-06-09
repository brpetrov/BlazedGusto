using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> IngredientDateRange(DateTime StartDate, DateTime EndDate);
        Task<bool> IngredientDelete(int IngredientId);
        Task<bool> IngredientInsert(Ingredient ingredient);
        Task<IEnumerable<Ingredient>> IngredientList();
        Task<IEnumerable<Ingredient>> IngredientSearch(string Param);
        Task<bool> IngredientUpdate(Ingredient ingredient);
        Task<Ingredient> Ingredient_GetOne(int IngredientId);
    }
}