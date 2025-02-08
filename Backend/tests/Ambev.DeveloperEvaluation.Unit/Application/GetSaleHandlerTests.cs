using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

public class GetSaleHandlerTests
{
    private readonly Mock<ISaleService> _saleServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleServiceMock = new Mock<ISaleService>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetSaleHandler(_saleServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedSale_WhenSaleExists()
    {
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);

        var saleEntity = new SalesEntity
        {
            Id = saleId,
            Client = "Cliente Teste",
            SaleNumber = "12345",
            TotalValue = 100.50m
        };

        var expectedResponse = new GetSaleResponse
        {
            Id = saleEntity.Id,
            Client = saleEntity.Client,
            SaleNumber = saleEntity.SaleNumber,
            TotalValue = saleEntity.TotalValue
        };

        _saleServiceMock.Setup(s => s.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(saleEntity);

        _mapperMock.Setup(m => m.Map<GetSaleResponse>(saleEntity))
            .Returns(expectedResponse);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResponse);

        _saleServiceMock.Verify(s => s.GetByIdAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<GetSaleResponse>(saleEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenSaleDoesNotExist()
    {
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);

        _saleServiceMock.Setup(s => s.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((SalesEntity)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeNull();

        _saleServiceMock.Verify(s => s.GetByIdAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<GetSaleResponse>(It.IsAny<SalesEntity>()), Times.Never);
    }
}
