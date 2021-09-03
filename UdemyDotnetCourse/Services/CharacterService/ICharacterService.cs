using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto addSkillRequest);

    }
}
