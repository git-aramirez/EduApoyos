using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IServices;
using EduApoyos.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EduApoyos.API.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly ISolicitudService _solicitudService;

        public SolicitudController(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudApoyoResponseDto>>> GetSolicitudes(
            [FromQuery] EstadoSolicitud? estado,
            [FromQuery] TipoApoyo? tipo)
        {
            var solicitudes = await _solicitudService.GetByEstadoAndTipoAsync(estado, tipo);

            return Ok(solicitudes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudDetalleResponseDto>> GetDetalle(Guid id)
        {
            var solicitud = await _solicitudService.GetByIdAsync(id);

            if (solicitud == null)
                return NotFound($"No se encontró la solicitud con id {id}.");

            return Ok(solicitud);
        }

        [HttpPost]
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
