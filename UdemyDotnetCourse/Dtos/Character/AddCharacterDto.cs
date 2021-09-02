﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 20;
        public int Intelligence { get; set; } = 20;
        public RpgClass Class { get; set; } = RpgClass.Kinght;
    }
}
