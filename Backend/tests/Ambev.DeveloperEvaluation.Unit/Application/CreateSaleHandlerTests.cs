using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using MediatR;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Application.Sale.SaleItem;

public class CreateSaleHandlerTests
{
    private readonly Mock<ISaleService> _saleServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleServiceMock = new Mock<ISaleService>();
        _mapperMock = new Mock<IMapper>();
        _mediatorMock = new Mock<IMediator>();

        _handler = new CreateSaleHandler(_saleServiceMock.Object, _mapperMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateSaleAndReturnResult()
    {
        var command = new CreateSaleCommand
        {
            Client = "Cliente Teste",
            Branch = "Filial 1",
            SaleNumber = "12345",
            TotalValue = 100.50m,
            Items = new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand { 
                    Product = "Produto A",
                    Quantity = 2,
                    UnitPrice = 50.25m
                }
            }
        };

        var saleEntity = new SalesEntity
        {
            Id = Guid.NewGuid(),
            Client = command.Client,
            Branch = command.Branch,
            SaleNumber = command.SaleNumber,
            TotalValue = command.TotalValue,
            Items = command.Items.Select(i => new SaleItemsEntity
            {
                Id = Guid.NewGuid(),
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        var expectedResult = new CreateSaleResult { Id = saleEntity.Id };

        _mapperMock.Setup(m => m.Map<SalesEntity>(command)).Returns(saleEntity);
        _saleServiceMock.Setup(s => s.CreateAsync(It.IsAny<SalesEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(saleEntity);
        _mapperMock.Setup(m => m.Map<CreateSaleResult>(saleEntity)).Returns(expectedResult);
        _mediatorMock.Setup(m => m.Publish(It.IsAny<SaleCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(saleEntity.Id);

        _mapperMock.Verify(m => m.Map<SalesEntity>(command), Times.Once);
        _saleServiceMock.Verify(s => s.CreateAsync(It.IsAny<SalesEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<CreateSaleResult>(saleEntity), Times.Once);
    }
}
