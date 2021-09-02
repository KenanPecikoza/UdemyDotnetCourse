using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public CharacterService(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }




        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var entity = _mapper.Map<Character>(newCharacter);
            _db.Add(entity);
            _db.SaveChanges();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _db.Characters.ToListAsync());
            return serviceResponse ;
        }

        public async Task< ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _db.Characters.ToListAsync());
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(await _db.Characters.FirstOrDefaultAsync(x=> x.Id==id));

            return serviceResponse;

        }

        public async  Task< ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _db.Characters.FirstAsync(x => x.Id == updatedCharacter.Id);
                _mapper.Map(updatedCharacter, character);
                _db.SaveChanges();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
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
                var character = await _db.Characters.FirstAsync(x => x.Id ==id);
                _db.Remove(character);
                _db.SaveChanges();
                serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_db.Characters);

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
