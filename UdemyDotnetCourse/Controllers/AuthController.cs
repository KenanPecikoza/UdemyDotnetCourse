using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotnetCourse.Data;
using UdemyDotnetCourse.Dtos.User;
using UdemyDotnetCourse.Models;

namespace UdemyDotnetCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register (UserRegisterDto request){
            var reponse = await _authRepo.Register(new User {Username= request.Username }, request.Password);
            if (!reponse.Success)
            {
                return BadRequest(reponse);
            }
            return reponse ;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var reponse = await _authRepo.Login(request.Username,request.Password);
            if (!reponse.Success)
            {
                return BadRequest(reponse);
            }
            return reponse;
        }


    }
}
