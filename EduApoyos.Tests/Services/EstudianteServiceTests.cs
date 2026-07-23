using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using EduApoyos.Application.Services;
using EduApoyos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Tests.Services
{
    public class EstudianteServiceTests
    {
        private readonly Mock<IEstudianteRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextMock;
        private readonly EstudianteService _service;

        public EstudianteServiceTests()
        {
            _repoMock = new Mock<IEstudianteRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextMock = new Mock<IHttpContextAccessor>();

            _service = new EstudianteService(
                _repoMock.Object,
                _mapperMock.Object,
                _httpContextMock.Object
            );
        }

        private void SetHttpContextWithUser(Guid userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = principal };
            _httpContextMock.Setup(h => h.HttpContext).Returns(context);
        }

        [Fact]
        public async Task AddAsync_ShouldMapAndSave()
        {
            var userId = Guid.NewGuid();
            SetHttpContextWithUser(userId);

            var request = new EstudianteRequestDto { NumeroDocumento = "123" };
            var entity = new EstudianteEntity { Id = Guid.NewGuid(), UsuarioId = userId };

            _mapperMock.Setup(m => m.Map<EstudianteEntity>(request)).Returns(entity);
            _repoMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<EstudianteResponseDto>(entity))
                .Returns(new EstudianteResponseDto { Id = entity.Id });

            var result = await _service.AddAsync(request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnDtos()
        {
            var entities = new List<EstudianteEntity> { new EstudianteEntity { Id = Guid.NewGuid() } };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<EstudianteResponseDto>>(entities))
                .Returns(new List<EstudianteResponseDto> { new EstudianteResponseDto { Id = entities[0].Id } });

            var result = await _service.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetSolicitudesByEstudianteIdAsync_ShouldReturnEmpty_WhenUserIdNull()
        {
            _httpContextMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());

            var result = await _service.GetSolicitudesByEstudianteIdAsync(Guid.NewGuid());

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetSolicitudesByEstudianteIdAsync_ShouldThrow_WhenUnauthorized()
        {
            var userId = Guid.NewGuid();
            SetHttpContextWithUser(userId);

            var estudiante = new EstudianteEntity { Id = Guid.NewGuid(), UsuarioId = Guid.NewGuid() }; // distinto
            _repoMock.Setup(r => r.GetByIdAsync(estudiante.Id)).ReturnsAsync(estudiante);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.GetSolicitudesByEstudianteIdAsync(estudiante.Id));
        }

        [Fact]
        public async Task GetSolicitudesByEstudianteIdAsync_ShouldReturnSolicitudes_WhenValid()
        {
            var userId = Guid.NewGuid();
            SetHttpContextWithUser(userId);

            var estudiante = new EstudianteEntity { Id = Guid.NewGuid(), UsuarioId = userId };
            var solicitudes = new List<SolicitudApoyoEntity> { new SolicitudApoyoEntity(estudiante.Id, Guid.NewGuid(), Domain.Enums.TipoApoyo.Beca, 500, "Test") };

            _repoMock.Setup(r => r.GetByIdAsync(estudiante.Id)).ReturnsAsync(estudiante);
            _repoMock.Setup(r => r.GetSolicitudesByEstudianteIdAsync(estudiante.Id)).ReturnsAsync(solicitudes);
            _mapperMock.Setup(m => m.Map<IEnumerable<SolicitudApoyoResponseDto>>(solicitudes))
                .Returns(new List<SolicitudApoyoResponseDto> { new SolicitudApoyoResponseDto { Id = solicitudes[0].Id } });

            var result = await _service.GetSolicitudesByEstudianteIdAsync(estudiante.Id);

            Assert.Single(result);
        }
    }
}
