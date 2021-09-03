using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Dtos.Fight;
using UdemyDotnetCourse.Dtos.Skill;
using UdemyDotnetCourse.Dtos.Weapon;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //Character
            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
            CreateMap<UpdateCharacterDto,Character>();
            //Weapon
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<UpdateWeaponDto, Weapon>();
            //Skill
            CreateMap<Skill, GetSkillDto>().ReverseMap();
            //HighScore
            CreateMap<Character, HighScoreDto>();
        }

    }
}
