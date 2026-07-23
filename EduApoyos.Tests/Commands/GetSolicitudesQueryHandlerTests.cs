using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApoyos.Tests.Commands
{
    using Xunit;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EduApoyos.Application.Commands;
    using EduApoyos.Domain.Entities;
    using EduApoyos.Domain.Enums;
    using Moq;
    using EduApoyos.Application.IRepositories;

    public class GetSolicitudesQueryHandlerTests
    {
        private readonly Mock<ISolicitudApoyoRepository> _solicitudRepoMock;
        private readonly GetSolicitudesQueryHandler _handler;

        public GetSolicitudesQueryHandlerTests()
        {
            _solicitudRepoMock = new Mock<ISolicitudApoyoRepository>();
            _handler = new GetSolicitudesQueryHandler(_solicitudRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSolicitudesFromRepository()
        {
            // Arrange: simulamos datos
            var simuladas = new List<SolicitudApoyoEntity>
        {
            new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 500, "desc")
            { Estado = EstadoSolicitud.Pendiente },
            new SolicitudApoyoEntity(Guid.NewGuid(), Guid.NewGuid(), TipoApoyo.Beca, 300, "desc")
            { Estado = EstadoSolicitud.Aprobada }
        };

            _solicitudRepoMock.Setup(r => r.GetByEstadoAndTipoAsync(null, null))
                .ReturnsAsync(simuladas);

            var query = new GetSolicitudesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Estado == EstadoSolicitud.Pendiente);
            Assert.Contains(result, s => s.Estado == EstadoSolicitud.Aprobada);
        }
    }
}
