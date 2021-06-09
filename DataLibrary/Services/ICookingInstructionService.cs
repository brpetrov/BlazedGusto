using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Services
{
    public interface ICookingInstructionService
    {
        Task<bool> CookingInstructionDelete(int CookingInstructionId);
        Task<bool> CookingInstructionInsert(CookingInstruction cookinginstruction);
        Task<IEnumerable<CookingInstruction>> CookingInstructionList();
        Task<IEnumerable<CookingInstruction>> CookingInstructionSearch(string Param);
        Task<bool> CookingInstructionUpdate(CookingInstruction cookinginstruction);
        Task<CookingInstruction> CookingInstruction_GetOne(int CookingInstructionId);
    }
}