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
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContext;
        public CharacterService(IMapper mapper, DataContext db, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _db = db;
            _httpContext = httpContext;
        }
        private int GetUserId()=>  int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var entity = _mapper.Map<Character>(newCharacter);
            entity.User = await _db.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
            _db.Add(entity);
             await _db.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _db.Characters.Where(x=> x.User.Id==GetUserId()).ToListAsync());
            return serviceResponse ;
        }

        public async Task< ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _db.Characters.Where(x=> x.User.Id==GetUserId()).ToListAsync());
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(await _db.Characters.FirstOrDefaultAsync(x=> x.Id==id && x.User.Id==GetUserId()));

            return serviceResponse;

        }

        public async  Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _db.Characters.Where(x=> x.UserId==GetUserId()).FirstOrDefaultAsync(x => x.Id == updatedCharacter.Id);
                if (character != null)
                {
                    _mapper.Map(updatedCharacter, character);
                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return  serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _db.Characters.FirstOrDefaultAsync(x => x.Id ==id && x.UserId==GetUserId());
                if (character!=null)
                {
                    _db.Remove(character);
                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_db.Characters.Where(x=> x.UserId==GetUserId()));
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";

                }


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
