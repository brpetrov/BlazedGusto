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
    public class MealService : IMealService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public MealService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Meal table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> MealInsert(Meal meal)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", meal.Title, DbType.String);
                parameters.Add("Img", meal.Img, DbType.String);
                parameters.Add("PrepTime", meal.PrepTime, DbType.Int32);
                parameters.Add("Nationality", meal.Nationality, DbType.String);
                parameters.Add("Description", meal.Description, DbType.String);
                parameters.Add("IngredientsId", meal.IngredientsId, DbType.Int32);
                parameters.Add("CookingInstructionsId", meal.CookingInstructionsId, DbType.Int32);
                parameters.Add("Created_At", meal.Created_At, DbType.DateTime);
                parameters.Add("Updated_At", meal.Updated_At, DbType.DateTime);

                // Stored procedure method
                await conn.ExecuteAsync("spMeal_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of meal rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Meal>> MealList()
        {
            IEnumerable<Meal> meals;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                meals = await conn.QueryAsync<Meal>("spMeal_List", commandType: CommandType.StoredProcedure);
            }
            return meals;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<Meal>> MealSearch(string @Param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<Meal> meals;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                meals = await conn.QueryAsync<Meal>("spMeal_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return meals;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<Meal>> MealDateRange(DateTime @StartDate, DateTime @EndDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<Meal> meals;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                meals = await conn.QueryAsync<Meal>("spMeal_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return meals;
        }

        // Get one meal based on its MealID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Meal> Meal_GetOne(int @MealId)
        {
            Meal meal = new Meal();
            var parameters = new DynamicParameters();
            parameters.Add("@MealId", MealId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                meal = await conn.QueryFirstOrDefaultAsync<Meal>("spMeal_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return meal;
        }
        // Update one Meal row based on its MealID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> MealUpdate(Meal meal)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("MealId", meal.MealId, DbType.Int32);

                parameters.Add("Title", meal.Title, DbType.String);
                parameters.Add("Img", meal.Img, DbType.String);
                parameters.Add("PrepTime", meal.PrepTime, DbType.Int32);
                parameters.Add("Nationality", meal.Nationality, DbType.String);
                parameters.Add("Description", meal.Description, DbType.String);
                parameters.Add("IngredientsId", meal.IngredientsId, DbType.Int32);
                parameters.Add("CookingInstructionsId", meal.CookingInstructionsId, DbType.Int32);
                parameters.Add("Created_At", meal.Created_At, DbType.DateTime);
                parameters.Add("Updated_At", meal.Updated_At, DbType.DateTime);

                await conn.ExecuteAsync("spMeal_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one Meal row based on its MealID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> MealDelete(int MealId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MealId", MealId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spMeal_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
