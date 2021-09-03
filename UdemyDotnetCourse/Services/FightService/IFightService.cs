using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Fight;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WapponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
        Task<ServiceResponse<List<HighScoreDto>>> GetHighscore();
    }
}
