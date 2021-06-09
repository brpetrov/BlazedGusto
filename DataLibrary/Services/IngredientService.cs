using Dapper;
using DataLibrary.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Services
{
    public class IngredientService : IIngredientService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public IngredientService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Ingredient table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> IngredientInsert(Ingredient ingredient)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Img", ingredient.Img, DbType.String);
                parameters.Add("Quantity", ingredient.Quantity, DbType.Decimal);
                parameters.Add("Type", ingredient.Type, DbType.String);
                parameters.Add("MealId", ingredient.MealId, DbType.Int32);

                // Stored procedure method
                await conn.ExecuteAsync("spIngredient_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of ingredient rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Ingredient>> IngredientList()
        {
            IEnumerable<Ingredient> ingredients;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                ingredients = await conn.QueryAsync<Ingredient>("spIngredient_List", commandType: CommandType.StoredProcedure);
            }
            return ingredients;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<Ingredient>> IngredientSearch(string @Param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<Ingredient> ingredients;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                ingredients = await conn.QueryAsync<Ingredient>("spIngredient_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return ingredients;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<Ingredient>> IngredientDateRange(DateTime @StartDate, DateTime @EndDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<Ingredient> ingredients;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                ingredients = await conn.QueryAsync<Ingredient>("spIngredient_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return ingredients;
        }

        // Get one ingredient based on its IngredientID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Ingredient> Ingredient_GetOne(int @IngredientId)
        {
            Ingredient ingredient = new Ingredient();
            var parameters = new DynamicParameters();
            parameters.Add("@IngredientId", IngredientId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                ingredient = await conn.QueryFirstOrDefaultAsync<Ingredient>("spIngredient_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return ingredient;
        }
        // Update one Ingredient row based on its IngredientID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> IngredientUpdate(Ingredient ingredient)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IngredientId", ingredient.IngredientId, DbType.Int32);

                parameters.Add("Img", ingredient.Img, DbType.String);
                parameters.Add("Quantity", ingredient.Quantity, DbType.Decimal);
                parameters.Add("Type", ingredient.Type, DbType.String);
                parameters.Add("MealId", ingredient.MealId, DbType.Int32);

                await conn.ExecuteAsync("spIngredient_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one Ingredient row based on its IngredientID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> IngredientDelete(int IngredientId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IngredientId", IngredientId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spIngredient_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
