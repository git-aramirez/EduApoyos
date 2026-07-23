using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduApoyos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet]
        [Authorize(Roles = "Asesor")]
        public async Task<ActionResult<IEnumerable<EstudianteResponseDto>>> GetEstudiantes()
        {
            var estudiantes = await _estudianteService.GetAllAsync();

            return Ok(estudiantes);
        }

        [HttpGet("{id}/solicitudes")]
        [Authorize(Roles = "Estudiante")]
        public async Task<ActionResult<IEnumerable<SolicitudApoyoResponseDto>>> GetSolicitudesDeEstudiante(Guid id)
        {
            var solicitudes = await _estudianteService.GetSolicitudesByEstudianteIdAsync(id);

            return Ok(solicitudes);
        }

        [HttpPost]
        [Authorize(Roles = "Asesor")]
        public async Task<ActionResult<EstudianteResponseDto>> Register([FromBody] EstudianteRequestDto estudianteRequestDto)
        {
            if (estudianteRequestDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevoEstudiante = await _estudianteService.AddAsync(estudianteRequestDto);

                if (nuevoEstudiante == null)
                    return Conflict("El estudiante ya existe o no pudo ser agregado.");

                return Ok(nuevoEstudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al crear estudiante: {ex.Message}");
            }
        }
    }
}
