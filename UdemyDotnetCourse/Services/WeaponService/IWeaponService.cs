using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Dtos.Weapon;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<List<GetWeaponDto>>> GetAll();
        Task<ServiceResponse<GetWeaponDto>> GetById(int id);
        Task<ServiceResponse<GetCharacterDto>> Add(AddWeaponDto newWeapon); 
        Task<ServiceResponse<GetWeaponDto>> Update(UpdateWeaponDto updatedWeapon);
        Task<ServiceResponse<List<GetWeaponDto>>> Delete(int id);


    }
}
