using Moq;
using Xunit;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Services;

public class CancelSaleHandlerTests
{
    private readonly Mock<ISaleService> _saleServiceMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleServiceMock = new Mock<ISaleService>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CancelSaleHandler(_saleServiceMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCancelSaleAndPublishEvent()
    {
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId, true);

        _saleServiceMock
            .Setup(s => s.CancelSaleAsync(saleId, true, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _saleServiceMock.Verify(s => s.CancelSaleAsync(saleId, true, It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Publish(It.Is<SaleCancelledEvent>(e => e.SaleId == saleId), It.IsAny<CancellationToken>()), Times.Once);
    }
}
