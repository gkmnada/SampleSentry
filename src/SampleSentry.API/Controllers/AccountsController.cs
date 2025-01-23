using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleSentry.API.Entities;
using SampleSentry.API.Features.ApplicationUser.Commands;
using SampleSentry.API.Models;
using SampleSentry.API.Tools;

namespace SampleSentry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountsController(IMediator mediator, JwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _mediator = mediator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(CreateApplicationUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Lütfen Kullanıcı Adı veya Şifrenizi Girin");
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (!result.Succeeded)
            {
                return BadRequest("Kullanıcı Adı veya Şifre Hatalı");
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Hesabınız Kilitlendi");
            }

            var tokenRequest = new JwtTokenRequest
            {
                UserID = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                Role = (await _userManager.GetRolesAsync(user)).ToList()
            };

            var token = _jwtTokenGenerator.GenerateToken(tokenRequest);

            return Ok(token);
        }
    }
}
