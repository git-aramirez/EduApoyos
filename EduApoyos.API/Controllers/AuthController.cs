using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IServices;
using EduApoyos.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduApoyos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioResponseDto>> Register([FromBody] UsuarioRequestDto usuarioRequestDto)
        {
            if (!ModelState.IsValid || usuarioRequestDto == null)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _usuarioService.AddAsync(usuarioRequestDto);

                if (usuario == null)
                    return Conflict("El usuario ya existe o no pudo ser agregado.");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _usuarioService.LoginAsync(model);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
