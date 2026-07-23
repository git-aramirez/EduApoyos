using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.Services;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Tests.Services
{
    public class SolicitudApoyoServiceTests
    {
        private readonly Mock<ISolicitudApoyoRepository> _solicitudRepoMock;
        private readonly Mock<IHistorialEstadoRepository> _historialRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SolicitudApoyoService _service;

        public SolicitudApoyoServiceTests()
        {
            _solicitudRepoMock = new Mock<ISolicitudApoyoRepository>();
            _historialRepoMock = new Mock<IHistorialEstadoRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new SolicitudApoyoService(
                _solicitudRepoMock.Object,
                _historialRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldMapAndSave()
        {
            var request = new SolicitudApoyoRequestDto { Descripcion = "Test" };
            var entity = new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test");

            _mapperMock.Setup(m => m.Map<SolicitudApoyoEntity>(request)).Returns(entity);
            _solicitudRepoMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<SolicitudApoyoResponseDto>(entity))
                .Returns(new SolicitudApoyoResponseDto { Id = entity.Id });

            var result = await _service.AddAsync(request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public async Task GetByEstadoAndTipoAsync_ShouldReturnDtos()
        {
            var entities = new List<SolicitudApoyoEntity>
        {
            new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test")
        };

            _solicitudRepoMock.Setup(r => r.GetByEstadoAndTipoAsync(null, null)).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<SolicitudApoyoResponseDto>>(entities))
                .Returns(new List<SolicitudApoyoResponseDto> { new SolicitudApoyoResponseDto { Id = entities[0].Id } });

            var result = await _service.GetByEstadoAndTipoAsync(null, null);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnWithHistorial_WhenFound()
        {
            var solicitudId = Guid.NewGuid();
            var entity = new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test");
            var historial = new List<HistorialEstadoEntity> { new HistorialEstadoEntity(solicitudId, Guid.NewGuid(), EstadoSolicitud.Pendiente, EstadoSolicitud.Aprobada, "Obs") };

            _solicitudRepoMock.Setup(r => r.GetByIdAsync(solicitudId)).ReturnsAsync(entity);
            _historialRepoMock.Setup(r => r.GetBySolicitudIdAsync(solicitudId)).ReturnsAsync(historial);

            var dto = new SolicitudDetalleResponseDto { Id = solicitudId };
            _mapperMock.Setup(m => m.Map<SolicitudDetalleResponseDto>(entity)).Returns(dto);
            _mapperMock.Setup(m => m.Map<IEnumerable<HistorialEstadoResponseDto>>(historial))
                .Returns(new List<HistorialEstadoResponseDto> { new HistorialEstadoResponseDto { Id = Guid.NewGuid() } });

            var result = await _service.GetByIdAsync(solicitudId);

            Assert.NotNull(result.historialEstados);
            Assert.Single(result.historialEstados);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEmpty_WhenNotFound()
        {
            var solicitudId = Guid.NewGuid();
            _solicitudRepoMock.Setup(r => r.GetByIdAsync(solicitudId)).ReturnsAsync((SolicitudApoyoEntity)null);

            var dto = new SolicitudDetalleResponseDto { Id = solicitudId };
            _mapperMock.Setup(m => m.Map<SolicitudDetalleResponseDto>(null)).Returns(dto);

            var result = await _service.GetByIdAsync(solicitudId);

            Assert.NotNull(result);
            Assert.Null(result.historialEstados);
        }

        [Fact]
        public async Task PatchEstadoAsync_ShouldThrow_WhenNotFound()
        {
            var solicitudId = Guid.NewGuid();
            _solicitudRepoMock.Setup(r => r.GetByIdAsync(solicitudId)).ReturnsAsync((SolicitudApoyoEntity)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.PatchEstadoAsync(solicitudId, new SolicitudCambioEstadoRequestDto { Estado = EstadoSolicitud.Aprobada }));
        }

        [Fact]
        public async Task PatchEstadoAsync_ShouldAddHistorial_WhenResponseNotNull()
        {
            var solicitudId = Guid.NewGuid();
            var solicitud = new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test");
            var response = new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test");

            _solicitudRepoMock.Setup(r => r.GetByIdAsync(solicitudId)).ReturnsAsync(solicitud);
            _solicitudRepoMock.Setup(r => r.PatchEstadoAsync(solicitud, EstadoSolicitud.Aprobada)).ReturnsAsync(response);
            _mapperMock.Setup(m => m.Map<SolicitudApoyoResponseDto>(response)).Returns(new SolicitudApoyoResponseDto { Id = response.Id });

            var result = await _service.PatchEstadoAsync(solicitudId, new SolicitudCambioEstadoRequestDto { Estado = EstadoSolicitud.Aprobada, Observacion = "Obs" });

            Assert.NotNull(result);
            _historialRepoMock.Verify(r => r.AddAsync(It.IsAny<HistorialEstadoEntity>()), Times.Once);
        }

        [Fact]
        public async Task PatchEstadoAsync_ShouldNotAddHistorial_WhenResponseNull()
        {
            var solicitudId = Guid.NewGuid();
            var solicitud = new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "Test");

            _solicitudRepoMock.Setup(r => r.GetByIdAsync(solicitudId)).ReturnsAsync(solicitud);
            _solicitudRepoMock.Setup(r => r.PatchEstadoAsync(solicitud, EstadoSolicitud.Aprobada)).ReturnsAsync((SolicitudApoyoEntity)null);
            _mapperMock.Setup(m => m.Map<SolicitudApoyoResponseDto>(null)).Returns(new SolicitudApoyoResponseDto());

            var result = await _service.PatchEstadoAsync(solicitudId, new SolicitudCambioEstadoRequestDto { Estado = EstadoSolicitud.Aprobada });

            Assert.NotNull(result);
            _historialRepoMock.Verify(r => r.AddAsync(It.IsAny<HistorialEstadoEntity>()), Times.Never);
        }
    }
}
