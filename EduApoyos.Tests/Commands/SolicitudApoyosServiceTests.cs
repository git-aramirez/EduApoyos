using EduApoyos.Application.Commands;
using EduApoyos.Application.IRepositories;
using EduApoyos.Domain.Entities;
using EduApoyos.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Tests.Commands
{
    public class CrearSolicitudCommandHandlerTests
    {
        private readonly Mock<ISolicitudApoyoRepository> _solicitudRepoMock;
        private readonly Mock<IEstudianteRepository> _estudianteRepoMock;
        private readonly CrearSolicitudCommandHandler _handler;

        public CrearSolicitudCommandHandlerTests()
        {
            _solicitudRepoMock = new Mock<ISolicitudApoyoRepository>();
            _estudianteRepoMock = new Mock<IEstudianteRepository>();

            _handler = new CrearSolicitudCommandHandler(
                _solicitudRepoMock.Object,
                _estudianteRepoMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEstudianteNotFound()
        {
            // Arrange
            _estudianteRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((EstudianteEntity)null);

            var command = new CrearSolicitudCommand
            {
                EstudianteId = Guid.NewGuid(),
                MontoSolicitado = 100,
                TipoApoyo = TipoApoyo.Beca
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenMontoIsInvalid()
        {
            _estudianteRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EstudianteEntity { Id = Guid.NewGuid() });

            var command = new CrearSolicitudCommand
            {
                EstudianteId = Guid.NewGuid(),
                MontoSolicitado = 0,
                TipoApoyo = TipoApoyo.Beca
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateSolicitud_WhenValid()
        {
            var estudianteId = Guid.NewGuid();
            _estudianteRepoMock.Setup(r => r.GetByIdAsync(estudianteId))
                .ReturnsAsync(new EstudianteEntity { Id = estudianteId });

            _solicitudRepoMock.Setup(r => r.AddAsync(It.IsAny<SolicitudApoyoEntity>()))
                    .ReturnsAsync((SolicitudApoyoEntity s) => s);

            var command = new CrearSolicitudCommand
            {
                EstudianteId = estudianteId,
                MontoSolicitado = 500,
                TipoApoyo = TipoApoyo.Beca
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(EstadoSolicitud.Pendiente, result.Estado);
            _solicitudRepoMock.Verify(r => r.AddAsync(It.IsAny<SolicitudApoyoEntity>()), Times.Once);
        }
    }
}
