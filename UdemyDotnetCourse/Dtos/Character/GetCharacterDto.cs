using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Skill;
using UdemyDotnetCourse.Dtos.Weapon;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 20;
        public int Intelligence { get; set; } = 20;
        public RpgClass Class { get; set; } = RpgClass.Kinght;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
