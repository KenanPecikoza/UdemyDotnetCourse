using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static Character kinght = new Character();
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id=1,Name="Sam", Class=RpgClass.Claric}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetById(int id)
        {
            return Ok(characters.FirstOrDefault(x=> x.Id==id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter([FromBody] Character newCharachter)
        {
            characters.Add(newCharachter);
            return Ok(characters);
        }
    }
}
