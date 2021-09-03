using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Dtos.Character;
using UdemyDotnetCourse.Dtos.Weapon;
using UdemyDotnetCourse.Models;
using UdemyDotnetCourse.Services.WeaponService;

namespace UdemyDotnetCourse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        IWeaponService _service;
        public WeaponController(IWeaponService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> Get()
        {
            return await _service.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> GetById(int id)
        {
            return await _service.GetById(id);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> Update(UpdateWeaponDto updateRequest)
        {
            return await _service.Update(updateRequest);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> Delete(int id)
        {
            return await _service.Delete(id);

        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Add(AddWeaponDto addRequest)
        {
            return await _service.Add(addRequest);
        }


    }
}
