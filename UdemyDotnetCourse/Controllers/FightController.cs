using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Fight;
using UdemyDotnetCourse.Models;
using UdemyDotnetCourse.Services.FightService;

namespace UdemyDotnetCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }
        [HttpPost("WeaponAttackt")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttackt (WeaponAttackDto request)
        {
            var response = await _fightService.WapponAttack(request);
            return response;
        }
        [HttpPost("SkillAttackt")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttackt(SkillAttackDto request)
        {
            var response = await _fightService.SkillAttack(request);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(FightRequestDto request)
        {
            return await _fightService.Fight(request);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<HighScoreDto>>>> GetHighscore()
        {
            return await _fightService.GetHighscore();
        }
    }
}
