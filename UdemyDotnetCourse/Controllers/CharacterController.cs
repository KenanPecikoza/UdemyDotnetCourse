using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Models;
using UdemyDotnetCourse.Services.CharacterService;

namespace UdemyDotnetCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;
        public CharacterController(ICharacterService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>>Get()
        {
            return await _service.GetAllCharacer();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
        {
            return await _service.GetCharacterById(id);
        }

        [HttpPost]
        public async Task< ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter([FromBody] AddCharacterDto newCharacter)
        {
            return await _service.AddCharacter(newCharacter);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _service.UpdateCharacter(updatedCharacter);
            if (response.Data==null)
            {
                return NotFound(response);
            }

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response = await _service.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return response;
        }


    }
}
