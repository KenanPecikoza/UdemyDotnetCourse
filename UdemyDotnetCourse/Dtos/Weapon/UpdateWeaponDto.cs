using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyDotnetCourse.Dtos.Weapon
{
    public class UpdateWeaponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
    }
}
