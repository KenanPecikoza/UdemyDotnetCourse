using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UdemyDotnetCourse.Data;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Dtos.Weapon;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _db;
        private readonly IMapper _autoMapper;
        private readonly IHttpContextAccessor _httpContext;

        public WeaponService(DataContext db, IMapper autoMapper,IHttpContextAccessor httpContext)
        {
            _db = db;
            _autoMapper = autoMapper;
            _httpContext = httpContext;

        }
        private int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<GetCharacterDto>>Add(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _db.Characters.FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId&& x.UserId==GetUserId());
                if (character==null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }
                var weapon = _autoMapper.Map<Weapon>(newWeapon);
                _db.Weapons.Add(weapon);
                await _db.SaveChangesAsync();
                var data = _autoMapper.Map<GetCharacterDto>(_db.Characters.Include(x=> x.Skills).Include(x=> x.Weapon).FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId && x.UserId == GetUserId()));
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
           
            return response;
        }

        public Task<ServiceResponse<List<GetWeaponDto>>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetWeaponDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetWeaponDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetWeaponDto>> Update(UpdateWeaponDto updatedWeapon)
        {
            throw new NotImplementedException();
        }
    }
}
