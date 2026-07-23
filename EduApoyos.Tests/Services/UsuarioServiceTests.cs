using AutoMapper;
using EduApoyos.Application.DTOs.Requests;
using EduApoyos.Application.DTOs.Responses;
using EduApoyos.Application.IRepositories;
using Microsoft.Extensions.Configuration;
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
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _configMock = new Mock<IConfiguration>();

            // Configuración simulada para JWT
            _configMock.Setup(c => c["Jwt:Key"]).Returns("c7f4d9a2c1e84f7a9d3c6b8e2f3a7c9d1e3f6a8b9c2d4e7f5a9b3c6d8e1f7a9b");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            _service = new UsuarioService(
                _repoMock.Object,
                _mapperMock.Object,
                _configMock.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldMapAndSave()
        {
            var request = new UsuarioRequestDto { UserName = "test", Password = "123456" };
            var entity = new UsuarioEntity { Id = Guid.NewGuid(), UserName = "test" };

            _mapperMock.Setup(m => m.Map<UsuarioEntity>(request)).Returns(entity);
            _repoMock.Setup(r => r.AddAsync(entity, request.Password)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<UsuarioResponseDto>(entity))
                .Returns(new UsuarioResponseDto { Id = entity.Id, UserName = entity.UserName });

            var result = await _service.AddAsync(request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal("test", result.UserName);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var loginDto = new LoginDto { Username = "noexistente", Password = "123" };
            _repoMock.Setup(r => r.GetByUserNameAsync(loginDto.Username)).ReturnsAsync((UsuarioEntity)null);

            var result = await _service.LoginAsync(loginDto);

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordInvalid()
        {
            var user = new UsuarioEntity { Id = Guid.NewGuid(), UserName = "test", NombreCompleto = "Test User", Rol = RolUsuario.Estudiante };
            var loginDto = new LoginDto { Username = "test", Password = "wrong" };

            _repoMock.Setup(r => r.GetByUserNameAsync(loginDto.Username)).ReturnsAsync(user);
            _repoMock.Setup(r => r.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(false);

            var result = await _service.LoginAsync(loginDto);

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenValidCredentials()
        {
            var user = new UsuarioEntity { Id = Guid.NewGuid(), UserName = "test", NombreCompleto = "Test User", Rol = RolUsuario.Estudiante };
            var loginDto = new LoginDto { Username = "test", Password = "Password9768$" };

            _repoMock.Setup(r => r.GetByUserNameAsync(loginDto.Username)).ReturnsAsync(user);
            _repoMock.Setup(r => r.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(true);

            var result = await _service.LoginAsync(loginDto);

            Assert.NotNull(result);
            Assert.Contains("ey", result); // JWT tokens empiezan con "eyJ..."
        }
    }
}
