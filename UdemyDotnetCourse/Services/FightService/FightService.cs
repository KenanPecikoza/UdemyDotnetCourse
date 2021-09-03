using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Data;
using UdemyDotnetCourse.Dtos.Fight;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.FightService
{
    public class FightService:IFightService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public FightService(DataContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto> 
            {
                Data = new FightResultDto()
            };
            try
            {
                var characters = await _db.Characters
                    .Include(x => x.Weapon)
                    .Include(x => x.Skills)
                    .Where(x => request.CharacherIds.Contains(x.Id)).ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;
                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeapponAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent,skill);
                        }
                        response.Data.Log
                            .Add($"{attacker.Name} attacks {opponent.Name} useing {attackUsed} with {(damage >= 0 ? damage : 0) } damage");
                        if (opponent.HitPoints<=0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defated");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} hp left");
                            break;
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _db.Characters
                    .Include(x => x.Skills).FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                var opponent = await _db.Characters.
                    FirstOrDefaultAsync(x => x.Id == request.OpponentId);
                var skill = attacker.Skills.FirstOrDefault(x => x.Id == request.SkillId);

                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} dosen't know this skill.";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _db.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }



        public async Task<ServiceResponse<AttackResultDto>> WapponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _db.Characters
                    .Include(x => x.Weapon).FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                var opponent = await _db.Characters.
                    FirstOrDefaultAsync(x => x.Id == request.OpponentId);
                int damage = DoWeapponAttack(attacker, opponent);
                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _db.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighscore()
        {
            var characters = await _db.Characters
                .Where(x => x.Fights > 0)
                .OrderByDescending(x => x.Victories)
                .ThenBy(c=> c.Defeats)
                .ToListAsync();
            var response = new ServiceResponse<List<HighScoreDto>> {

                Data = _mapper.Map<List<HighScoreDto>>(characters)
            };
            return response;
        }


        //static method
        private static int DoWeapponAttack(Character attacker, Character opponent)
        {
            int damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
            damage -= new Random().Next(opponent.Defense);
            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }
        private static int DoSkillAttack(Character attacker, Character opponent, Skill skill)
        {
            int damage = skill.Damage + new Random().Next(attacker.Intelligence);
            damage -= new Random().Next(opponent.Defense);
            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

    }
}
