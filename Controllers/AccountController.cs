using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProcessoManyminds_Back.Models.DTOs;
using System.Security.Claims;

namespace ProcessoManyminds_Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Authorized")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("/");
        }

        [HttpGet("Login")]
        public IActionResult GetPageLogin(string returnUrl)
        {
            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO user)
        {
            try
            {
                var userAuthenticated = await _userManager.GetUserAsync(User);

                if(userAuthenticated is not null) throw new ArgumentException("Este usuário já está logado!");

                var userIdentity = await _userManager.FindByNameAsync(user.Name);
                if (userIdentity == null)
                {
                    throw new ArgumentException("Nome ou senha incorretos!");
                }

                var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(userIdentity);

                if (lockoutEndDate is not null && lockoutEndDate > DateTimeOffset.UtcNow) throw new ArgumentException("Conta bloqueada temporariamente!");

                var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(userIdentity, user.Password, true);

                lockoutEndDate = await _userManager.GetLockoutEndDateAsync(userIdentity);

                if (lockoutEndDate is not null && lockoutEndDate > DateTimeOffset.UtcNow) throw new ArgumentException("Conta bloqueada temporariamente!");

                if (isPasswordValid.Succeeded)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, userIdentity.Id),
                        new Claim(ClaimTypes.Name, userIdentity.UserName),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                        });
                    return Ok(new { uri = "/", User = new { userIdentity.Id, Name = userIdentity.UserName } });
                }
                else
                {
                    //await _userManager.AccessFailedAsync(userIdentity);

                    throw new ArgumentException("Nome ou senha incorretos!");
                }
            } catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UsuarioDTO user)
        {
            try
            {
                if (await _userManager.FindByNameAsync(user.Name) != null)
                {
                    throw new ArgumentException("Este nome já está cadastrado");
                }
                var identityUser = new IdentityUser
                {
                    UserName = user.Name
                };

                identityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, user.Password);
                var result = await _userManager.CreateAsync(identityUser);
                if (result.Succeeded)
                {
                    await _userManager.SetLockoutEnabledAsync(identityUser, false);
                    return Ok();
                }

            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Deslogar")]
        public async Task<IActionResult> Deslogar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
