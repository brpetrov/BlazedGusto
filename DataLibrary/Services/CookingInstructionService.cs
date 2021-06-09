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
    public class CookingInstructionService : ICookingInstructionService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public CookingInstructionService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a CookingInstruction table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CookingInstructionInsert(CookingInstruction cookinginstruction)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Img", cookinginstruction.Img, DbType.String);
                parameters.Add("Step", cookinginstruction.Step, DbType.Int32);
                parameters.Add("Description", cookinginstruction.Description, DbType.String);
                parameters.Add("MealId", cookinginstruction.MealId, DbType.Int32);

                // Stored procedure method
                await conn.ExecuteAsync("spCookingInstruction_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of cookinginstruction rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<CookingInstruction>> CookingInstructionList()
        {
            IEnumerable<CookingInstruction> cookinginstructions;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                cookinginstructions = await conn.QueryAsync<CookingInstruction>("spCookingInstruction_List", commandType: CommandType.StoredProcedure);
            }
            return cookinginstructions;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<CookingInstruction>> CookingInstructionSearch(string @Param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<CookingInstruction> cookinginstructions;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                cookinginstructions = await conn.QueryAsync<CookingInstruction>("spCookingInstruction_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return cookinginstructions;
        }

        // Get one cookinginstruction based on its CookingInstructionID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<CookingInstruction> CookingInstruction_GetOne(int @CookingInstructionId)
        {
            CookingInstruction cookinginstruction = new CookingInstruction();
            var parameters = new DynamicParameters();
            parameters.Add("@CookingInstructionId", CookingInstructionId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                cookinginstruction = await conn.QueryFirstOrDefaultAsync<CookingInstruction>("spCookingInstruction_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return cookinginstruction;
        }
        // Update one CookingInstruction row based on its CookingInstructionID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CookingInstructionUpdate(CookingInstruction cookinginstruction)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("CookingInstructionId", cookinginstruction.CookingInstructionId, DbType.Int32);

                parameters.Add("Img", cookinginstruction.Img, DbType.String);
                parameters.Add("Step", cookinginstruction.Step, DbType.Int32);
                parameters.Add("Description", cookinginstruction.Description, DbType.String);
                parameters.Add("MealId", cookinginstruction.MealId, DbType.Int32);

                await conn.ExecuteAsync("spCookingInstruction_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one CookingInstruction row based on its CookingInstructionID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CookingInstructionDelete(int CookingInstructionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CookingInstructionId", CookingInstructionId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spCookingInstruction_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
