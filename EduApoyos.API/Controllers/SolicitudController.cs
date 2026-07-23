using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduApoyos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudApoyoService _solicitudService;

        public SolicitudController(ISolicitudApoyoService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        [Authorize(Roles = "Asesor")]
        public async Task<ActionResult<IEnumerable<SolicitudApoyoResponseDto>>> GetSolicitudes(
            [FromQuery] EstadoSolicitud? estado,
            [FromQuery] TipoApoyo? tipo)
        {
            var solicitudes = await _solicitudService.GetByEstadoAndTipoAsync(estado, tipo);

            return Ok(solicitudes);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Asesor")]
        public async Task<ActionResult<SolicitudDetalleResponseDto>> GetDetalle(Guid id)
        {
            var solicitud = await _solicitudService.GetByIdAsync(id);

            if (solicitud == null)
                return NotFound($"No se encontró la solicitud con id {id}.");

            return Ok(solicitud);
        }

        [HttpPost]
        [Authorize(Roles = "Asesor,Estudiante")]
        public async Task<ActionResult<SolicitudApoyoResponseDto>> Crear([FromBody] SolicitudApoyoRequestDto solicitudApoyoRequestDto)
        {
            if (solicitudApoyoRequestDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevaSolicitud = await _solicitudService.AddAsync(solicitudApoyoRequestDto);

                if (nuevaSolicitud == null)
                    return Conflict("La solicitud no pudo ser creada, posiblemente ya existe o no cumple las reglas de negocio.");

                return Ok(nuevaSolicitud);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al crear solicitud: {ex.Message}");
            }
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "Asesor")]
        public async Task<ActionResult<SolicitudApoyoResponseDto>> CambiarEstado(Guid id, [FromBody] SolicitudCambioEstadoRequestDto solicitudCambioEstadoRequestDto)
        {
            if (solicitudCambioEstadoRequestDto == null || !ModelState.IsValid)
                return BadRequest("Debe enviar el estado y una observación.");

            var solicitud = await _solicitudService.PatchEstadoAsync(id, solicitudCambioEstadoRequestDto);

            if (solicitud == null)
                return NotFound($"No se encontró la solicitud con id {id}.");

            return Ok(solicitud);
        }

    }
}
